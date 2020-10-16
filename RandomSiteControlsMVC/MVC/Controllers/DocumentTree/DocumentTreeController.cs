using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using RandomSiteControlsMVC.MVC.Models.DocumentTree;
using Telerik.Sitefinity;
using System.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Security.Claims;
using System.Collections.Generic;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using ServiceStack;

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "DocumentTreeMVC", Title = "Document Tree", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfListitemsIcn sfMvcIcn")]
    public class DocumentTreeController : Controller, ICustomWidgetVisualization
    {
        public ActionResult Index()
        {
            DocumentTreeModel model = this.GetModel();

            return View(this.TemplateName, model);
        }

        private DocumentTreeModel GetModel()
        {
            if (!String.IsNullOrEmpty(this.SerializedSelectedItemId))
            {
                dynamic item = DynamicJson.Deserialize(this.SerializedSelectedItem);
                if (Guid.Parse(item.RootId) == Guid.Empty)
                {
                    //Album
                    this.LibraryId = new Guid(this.SerializedSelectedItemId);   
                }
                else
                {
                    //Folder
                    this.LibraryId = new Guid(item.RootId);
                    this.FolderId = new Guid(item.Id);
                }

                this.SelectedLibraryName = item.Title;
            }

            var model = new DocumentTreeModel();

            model.Animated = this.Animated;
            model.RenderParent = this.RenderParent;
            model.ExpandLevelDepth = this.ExpandLevelDepth;
            model.Nodes.AddRange(this.SetTreeNodeDocumentLibrariesAndFolders());
            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View(this.TemplateName, this.GetModel()).ExecuteResult(this.ControllerContext);
        }


        public List<DocumentTreeNode> SetTreeNodeDocumentLibrariesAndFolders()
        {
            List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();
            LibrariesManager manager = new LibrariesManager();

            //Folder Focused
            if (this.FolderId != Guid.Empty)
            {
                var album = manager.GetDocumentLibrary(this.LibraryId);
                if (album != null)
                {
                    var selectedFolder = manager.GetFolder(this.FolderId);
                    if (selectedFolder != null)
                    {
                        this.ParseSingleFolder(nodes, manager, album, selectedFolder);
                    }
                    else
                    {
                        throw new Exception("Invalid FolderId Specified or Unable to find folder");
                    }
                }
                else
                {
                    throw new Exception("Invalid LibraryId Specified or Unable to find library");
                }
            }
            else
            {
                if (this.LibraryId != Guid.Empty)
                {
                    //Libary Focused
                    var album = manager.GetDocumentLibrary(this.LibraryId);
                    if (album != null)
                    {
                        this.ParseSingleAlbum(nodes, manager, album);
                    }
                    else
                    {
                        throw new Exception("Invalid LibraryId Specified");
                    }
                }
            }

            //Bind to the tree
            return nodes;
        }

        private void ParseSingleFolder(List<DocumentTreeNode> nodes, LibrariesManager manager, DocumentLibrary album, IFolder folder)
        {
            var folders = manager.GetAllFolders(album);

            DocumentTreeNode folderNode = new DocumentTreeNode();
            folderNode.Title = folder.Title;
            folderNode.Expanded = this.Expanded;
            folderNode.Id = folder.Id;
            folderNode.IsFolder = true;
            folderNode.CssClass += " album";
            nodes.Add(folderNode);

            //Get Documents for this Library
            var docsInLibrary = manager.GetDocuments().Where(x => x.Parent.Id == album.Id && x.FolderId == folder.Id).Where(this.FilterExpressionForDocuments);
            foreach (var doc in docsInLibrary)
            {
                DocumentTreeNode docNode = this.CreateDocNode(album, doc);
                folderNode.Nodes.Add(docNode);
            }

            //Load Children
            foreach (var child in folders.Where(x => x.ParentId == folder.Id))
            {
                folderNode.Nodes.Add(this.GetChildNodes(child, folders, album, manager, docsInLibrary));
            }
        }


        private void ParseSingleAlbum(List<DocumentTreeNode> nodes, LibrariesManager manager, DocumentLibrary album)
        {
            DocumentTreeNode rootNode = new DocumentTreeNode();
            rootNode.Title = album.Title;
            rootNode.Expanded = this.Expanded;
            rootNode.Id = album.Id;
            rootNode.IsFolder = true;
            rootNode.CssClass += " album";
            nodes.Add(rootNode);

            //Get Documents for this Library
            var docsInLibrary = manager.GetDocuments().Where(x => x.Parent.Id == album.Id && x.FolderId == null).Where(this.FilterExpressionForDocuments);
            foreach (var doc in docsInLibrary)
            {
                DocumentTreeNode docNode = this.CreateDocNode(album, doc);
                rootNode.Nodes.Add(docNode);
            }

            //Create nodes for all the folders
            var folders = manager.GetAllFolders(album);

            //Loop through all root folders
            foreach (var folder in folders.Where(x => x.ParentId == null))
            {
                var folderNode = new DocumentTreeNode();
                folderNode.Title = folder.Title;
                folderNode.Expanded = this.Expanded;
                folderNode.Id = folder.Id;
                folderNode.IsFolder = true;
                folderNode.CssClass += " folder";

                //Load Documents
                var docsInFolder = manager.GetDocuments().Where(x => x.Parent.Id == album.Id && x.FolderId == folder.Id).Where(this.FilterExpressionForDocuments);
                foreach (var doc in docsInFolder)
                {
                    DocumentTreeNode docNode = this.CreateDocNode(album, doc);
                    folderNode.Nodes.Add(docNode);
                }

                //Load Children
                foreach (var child in folders.Where(x => x.ParentId == folder.Id))
                {
                    folderNode.Nodes.Add(this.GetChildNodes(child, folders, album, manager, docsInLibrary));
                }

                rootNode.Nodes.Add(folderNode);
            }
        }

        private DocumentTreeNode GetChildNodes(IFolder folder, IQueryable<IFolder> folders, DocumentLibrary album, LibrariesManager manager, IQueryable<Document> docsInLibrary)
        {
            var folderNode = new DocumentTreeNode();
            folderNode.Expanded = this.Expanded;
            folderNode.Title = folder.Title;
            folderNode.Id = folder.Id;
            folderNode.IsFolder = true;
            folderNode.CssClass += " folder";

            //Load Documents
            var docsInFolder = manager.GetDocuments().Where(x => x.Parent.Id == album.Id && x.FolderId == folder.Id).Where(this.FilterExpressionForDocuments);
            foreach (var doc in docsInFolder)
            {
                DocumentTreeNode docNode = this.CreateDocNode(album, doc);
                folderNode.Nodes.Add(docNode);
            }

            //Load Children
            foreach (var child in folders.Where(x => x.ParentId == folder.Id))
            {
                folderNode.Nodes.Add(this.GetChildNodes(child, folders, album, manager, docsInLibrary));
            }

            return folderNode;
        }

        private DocumentTreeNode CreateDocNode(Telerik.Sitefinity.Libraries.Model.DocumentLibrary item, Telerik.Sitefinity.Libraries.Model.Document doc)
        {
            var docNode = new DocumentTreeNode();
            docNode.Title = doc.Title.ToString();
            docNode.NavigateUrl = doc.MediaUrl;
            docNode.Target = this.Target;
            docNode.Id = item.Id;
            docNode.ParentId = item.ParentId;
            docNode.Extension = doc.Extension.Replace(".", "").ToLower();
            docNode.IsFolder = false;
            docNode.ContentCssClass = "sfdownloadTitle";
            docNode.CssClass += " sfdownloadFile sf{0}".Arrange(doc.Extension.Replace(".", "").ToLower());
            return docNode;
        }

        #region PROPERTIES
        public string SerializedSelectedItemId { get; set; }
        public string SerializedSelectedItem { get; set; }

        public string SelectedLibraryName { get; set; }

        public int ExpandLevelDepth { get; set; } = 2;


        public Guid LibraryId { get; set; } = Guid.Empty;
        public Guid FolderId { get; set; } = Guid.Empty;

        public string FilterExpressionForDocuments { get; set; } = "Visible == True && Status == Live";

        public bool Expanded { get; set; } = true;

        public bool Animated { get; set; } = false;

        public string Target { get; set; } = "_blank";

        public bool RenderParent { get; set; } = true;
        public string TemplateName { get; set; } = "Treeview";


        #region helpers
        protected bool IsAnonymous
        {
            get
            {
                var identity = ClaimsManager.GetCurrentIdentity();
                return identity.IsAuthenticated ? false : true;
            }
        }

        protected Guid UserId
        {
            get
            {
                var identity = ClaimsManager.GetCurrentIdentity();
                return identity.UserId;
            }
        }
        #endregion
        #endregion

        #region ICustomWidgetVisualization
        public bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(this.SerializedSelectedItemId);
            }
        }

        public string EmptyLinkText
        {
            get
            {
                return "Select a library";
            }
        }
        #endregion
    }
}