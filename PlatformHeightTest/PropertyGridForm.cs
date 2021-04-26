using System.Windows.Forms;

namespace PlatformHeightTest
{
    public partial class PropertyGridForm : Form
    {
        public PropertyGridForm()
        {
            InitializeComponent();
        }

        public void SetPropertyGridForm(PlatformHeightTest.XlsxFormat xlsxFormat)
        {
            propertyGrid1.SelectedObject = xlsxFormat;
        }
    }
}