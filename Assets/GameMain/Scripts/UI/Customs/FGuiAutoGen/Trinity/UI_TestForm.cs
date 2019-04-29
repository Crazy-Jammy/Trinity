/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Trinity
{
	public partial class UI_TestForm : GComponent
	{
		public GGraph m_n2;
		public GImage m_n1;
		public UI_BtnBag m_BtnBag;

		public const string URL = "ui://sgzdq45znlwe0";

		public static UI_TestForm CreateInstance()
		{
			return (UI_TestForm)UIPackage.CreateObject("Trinity","TestForm");
		}

		public UI_TestForm()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_n2 = (GGraph)this.GetChild("n2");
			m_n1 = (GImage)this.GetChild("n1");
			m_BtnBag = (UI_BtnBag)this.GetChild("BtnBag");
		}
	}
}