/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Trinity
{
	public partial class UI_BtnBag : GButton
	{
		public Controller m_button;
		public GImage m_n0;

		public const string URL = "ui://sgzdq45znlwe3";

		public static UI_BtnBag CreateInstance()
		{
			return (UI_BtnBag)UIPackage.CreateObject("Trinity","BtnBag");
		}

		public UI_BtnBag()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_button = this.GetController("button");
			m_n0 = (GImage)this.GetChild("n0");
		}
	}
}