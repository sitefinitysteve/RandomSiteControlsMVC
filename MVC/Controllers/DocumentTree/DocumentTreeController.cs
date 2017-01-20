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

namespace SitefinityWebApp.Mvc.Controllers
{
    [EnhanceViewEnginesAttribute]
    [ControllerToolboxItem(Name = "DocumentTreeMVC", Title = "Document Tree", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = "sfListitemsIcn sfMvcIcn")]
    public class DocumentTreeController : Controller, ICustomWidgetVisualization
    {
        private string _cacheKey = "DocumentFolderList-LibId:{0}-FolderId:{1}-IsAnon:{2}-UserId:{3}";

        public ActionResult Index()
        {
            DocumentTreeModel model = GetModel();

            return View("Default", model);
        }

        private DocumentTreeModel GetModel()
        {
            var model = new DocumentTreeModel();

            model.RenderMode = this.RenderMode;
            model.ExpandLevelDepth = this.ExpandLevelDepth;
            model.Nodes.AddRange(this.SetTreeNodeDocumentLibrariesAndFolders());
            return model;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            View("Default", this.GetModel()).ExecuteResult(this.ControllerContext);
        }

        private void CreateCacheKey()
        {
            _cacheKey = _cacheKey.Arrange(this.LibraryId, this.FolderId, this.IsAnonymous, this.UserId);
        }


        public List<DocumentTreeNode> SetTreeNodeDocumentLibrariesAndFolders()
        {
            List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();
            if (RSCUtil.Cache.Contains(_cacheKey))
            {
                //Load Cache
                nodes = RSCUtil.Cache[_cacheKey] as List<DocumentTreeNode>;
            }
            else
            {
                //Create Cache
                LibrariesManager manager = new LibrariesManager();

                //Folder Focused
                if (this.FolderId != Guid.Empty)
                {
                    var album = manager.GetDocumentLibrary(this.LibraryId);
                    if (album != null)
                    {
                        var folders = manager.GetAllFolders(album);
                        var selectedFolder = folders.FirstOrDefault(x => x.Id == this.FolderId);
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

                if (!SystemManager.IsDesignMode && RSCUtil.SfsConfig.CacheTimeoutMinutes > 0)
                {
                    //Add to cache
                    RSCUtil.AddToCache(nodes, _cacheKey, TimeSpan.FromMinutes(RSCUtil.SfsConfig.CacheTimeoutMinutes));
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
            this.SetImage(folderNode);
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
            this.SetImage(rootNode);
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
                this.SetImage(folderNode);
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
            this.SetImage(folderNode);
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

        private void SetImage(DocumentTreeNode node)
        {
            #region IMAGES
            if (!String.IsNullOrEmpty(this.FolderImageUrl))
            {
                node.ImageUrl = this.FolderImageUrl;
            }

            if (!String.IsNullOrEmpty(this.FolderExpandedImageUrl))
            {
                node.ExpandedImageUrl = this.FolderExpandedImageUrl;
            }
            #endregion
        }

        #region PROPERTIES
        private Guid _libraryId = Guid.Empty;
        public Guid LibraryId
        {
            get
            {
                return _libraryId;
            }
            set
            {
                _libraryId = value;
            }
        }

        string _selectedLibraryName;
        public string SelectedLibraryName
        {
            get { return _selectedLibraryName; }
            set
            {
                _selectedLibraryName = value;
            }
        }

        int _expandLevelsToInclude = 2;
        public int ExpandLevelDepth
        {
            get { return _expandLevelsToInclude; }
            set
            {
                _expandLevelsToInclude = value;
            }
        }


        private Guid _folderId = Guid.Empty;
        public Guid FolderId
        {
            get
            {
                return _folderId;
            }
            set
            {
                _folderId = value;
            }
        }

        string _documentFilterExpression = "Visible == True && Status == Live";
        public string FilterExpressionForDocuments
        {
            get { return _documentFilterExpression; }
            set
            {
                _documentFilterExpression = value;
            }
        }

        bool _expanded = true;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;
            }
        }

        StyleEnum _style = StyleEnum.TreeView;
        public StyleEnum RenderMode
        {
            get { return _style; }
            set
            {
                _style = value;
            }
        }


        string _folderImageUrl;
        public string FolderImageUrl
        {
            get { return _folderImageUrl; }
            set
            {
                _folderImageUrl = value;
            }
        }

        string _folderExpandedImageUrl;
        public string FolderExpandedImageUrl
        {
            get { return _folderExpandedImageUrl; }
            set
            {
                _folderExpandedImageUrl = value;
            }
        }

        string _iconSize = "Small";
        public string IconSize
        {
            get { return _iconSize; }
            set
            {
                _iconSize = value;
            }
        }

        string _target = "_blank";
        public string Target
        {
            get { return _target; }
            set
            {
                _target = value;
            }
        }

        bool _showLineImages = false;
        public bool ShowLineImages
        {
            get { return _showLineImages; }
            set
            {
                _showLineImages = value;
            }
        }


        public bool IsAnonymous
        {
            get
            {
                var identity = ClaimsManager.GetCurrentIdentity();
                return identity.IsAuthenticated ? false : true;
            }
        }

        public Guid UserId
        {
            get
            {
                var identity = ClaimsManager.GetCurrentIdentity();
                return identity.UserId;
            }
        }
        #endregion

        #region ICustomWidgetVisualization
        public bool IsEmpty
        {
            get
            {
                return this.LibraryId == Guid.Empty;
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