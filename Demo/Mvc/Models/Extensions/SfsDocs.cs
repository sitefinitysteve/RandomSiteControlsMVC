using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Thanks https://xmltocsharp.azurewebsites.net/
/// rsc-build-docs.xml is exported from the RSC build process.  The extensions controller
/// then reads it into this class
/// </summary>

namespace SitefinityWebApp.Mvc.Models.Extensions
{
	[DataContract(Namespace = ".")]
	[XmlRoot(ElementName = "doc")]
	public class Doc
	{
		[XmlElement(ElementName = "assembly")]
		public Assembly Assembly { get; set; }
		[XmlElement(ElementName = "members")]
		public Members Members { get; set; }
	}

	[XmlRoot(ElementName = "assembly")]
	public class Assembly
	{
		[XmlElement(ElementName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "members")]
	public class Members
	{
		[XmlElement(ElementName = "member")]
		public List<Member> Member { get; set; }
	}

	[XmlRoot(ElementName = "member")]
	public class Member
	{
		[XmlElement(ElementName = "summary")]
		public string Summary { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "returns")]
		public string Returns { get; set; }
		[XmlElement(ElementName = "param")]
		public List<Param> Param { get; set; }
		[XmlElement(ElementName = "typeparam")]
		public Typeparam Typeparam { get; set; }
		[XmlElement(ElementName = "example")]
		public string Example { get; set; }
	}

	[XmlRoot(ElementName = "param")]
	public class Param
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "typeparam")]
	public class Typeparam
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
		[XmlText]
		public string Text { get; set; }
	}
}
