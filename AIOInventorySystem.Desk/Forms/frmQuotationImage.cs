using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmQuotationImage : Form
    {
        byte[] @photologo, @photologo1, @photologo2;

        public frmQuotationImage()
        {
            InitializeComponent();
            maxproductid();
            getData(CommonMethod.CompId);
        }

        public void maxproductid()
        {
            try
            {
                QuotationImageMasterRepository productrepo = new QuotationImageMasterRepository();
                var mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ImageId));
                if (mid == null)
                    txtQutNo.Text = "1";
                else
                    txtQutNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        void conv_photologo()
        {
            @photologo = null;
            @photologo1 = null;
            if (picLogo.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                picLogo.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photologo = photo_aray;
            }
            if (picLogo1.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                picLogo1.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photologo1 = photo_aray;
            }
            if (picLogo2.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                picLogo2.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photologo2 = photo_aray;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    picLogo.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    picLogo1.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSaveUpdate.Text == "Save")
                {
                    QuotationImageMasterRepository qutImageRepo = new QuotationImageMasterRepository();
                    tnlQuotationImge qutImageData = new tnlQuotationImge();
                    qutImageData.ImageId = Convert.ToInt32(txtQutNo.Text);
                    conv_photologo();
                    qutImageData.Image1 = @photologo;
                    qutImageData.Image2 = @photologo1;
                    qutImageData.MainImage = @photologo2;
                    qutImageData.CompId = CommonMethod.CompId;
                    qutImageRepo.Add(qutImageData);
                    qutImageRepo.Save();
                    MessageBox.Show("Record Save Successfully");
                    getData(CommonMethod.CompId);
                }
                else
                {
                    QuotationImageMasterRepository qutImageRepo = new QuotationImageMasterRepository();
                    tnlQuotationImge qutImageData = qutImageRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (qutImageData != null)
                    {
                        qutImageData.ImageId = Convert.ToInt32(txtQutNo.Text);
                        conv_photologo();
                        qutImageData.Image1 = @photologo;
                        qutImageData.Image2 = @photologo1;
                        qutImageData.MainImage = @photologo2;
                        qutImageData.CompId = CommonMethod.CompId;
                        qutImageRepo.Edit(qutImageData);
                        qutImageRepo.Save();
                    }
                    MessageBox.Show("Record Update Successfully");
                    getData(CommonMethod.CompId);
                }
            }
            catch (Exception)
            { }
        }

        public void getData(int? compId)
        {
            try
            {
                QuotationImageMasterRepository qutImageRepo = new QuotationImageMasterRepository();
                tnlQuotationImge qutImageData = qutImageRepo.GetAll().Where(t => t.CompId == compId).FirstOrDefault();
                if (qutImageData != null)
                {
                    txtQutNo.Text = Convert.ToString(qutImageData.ImageId);
                    if (qutImageData.Image1 != null)
                    {
                        @photologo = (byte[])qutImageData.Image1;
                        MemoryStream ms = new MemoryStream(@photologo);
                        picLogo.Image = Image.FromStream(ms);
                    }
                    else
                        picLogo.Image = null;
                    if (qutImageData.Image2 != null)
                    {
                        @photologo1 = (byte[])qutImageData.Image2;
                        MemoryStream ms = new MemoryStream(@photologo1);
                        picLogo1.Image = Image.FromStream(ms);
                    }
                    else
                        picLogo1.Image = null;
                    if (qutImageData.MainImage != null)
                    {
                        @photologo2 = (byte[])qutImageData.MainImage;
                        MemoryStream ms = new MemoryStream(@photologo2);
                        picLogo2.Image = Image.FromStream(ms);
                    }
                    else
                        picLogo2.Image = null;
                    btnSaveUpdate.Text = "Update";
                    this.ActiveControl = btnSaveUpdate;
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnImage3_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    picLogo2.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }
    }
}