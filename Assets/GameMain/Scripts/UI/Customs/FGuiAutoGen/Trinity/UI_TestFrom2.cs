/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Trinity
{
	public partial class UI_TestFrom2 : GComponent
	{
		public GGraph m_n2;
		public GImage m_n0;
		public GImage m_n3;

		public const string URL = "ui://sgzdq45zol7f4";

		public static UI_TestFrom2 CreateInstance()
		{
			return (UI_TestFrom2)UIPackage.CreateObject("Trinity","TestFrom2");
		}

		public UI_TestFrom2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_n2 = (GGraph)this.GetChild("n2");
			m_n0 = (GImage)this.GetChild("n0");
			m_n3 = (GImage)this.GetChild("n3");
		}
	}
}