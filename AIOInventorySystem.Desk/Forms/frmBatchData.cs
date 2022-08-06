using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBatchData : Form
    {
        DbClass db = new DbClass();
        string pname, cname, unit, batch, expiry;
        decimal Quantity = 0, remqty = 0;
        string[] pono = new string[3];
        public int k = 1, m = 0;

        public frmBatchData()
        {
            InitializeComponent();
        }

        public frmBatchData(string Proname, string company, string unitname, decimal qty)
        {
            InitializeComponent();
            pname = Proname;
            cname = company;
            unit = unitname;
            Quantity = qty;
            LoadData(pname, cname, unit);
        }

        public void LoadData(string p, string c, string u)
        {
            try
            {
                //start of fillgrid with batchwise quantity
                PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
                BatchRepository bRepo = new BatchRepository();
                List<PurchaseDetail> pList = pDetailRepo.GetAll().Where(t => t.ProductName == p && t.CompanyName == c && t.Unit == u && t.Status == "Active" && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                int i = 0;
                foreach (var item in pList)
                {
                    object ExpDate = "";
                    if (item.ExpireDate != null)
                    {
                        if (item.ExpireDate.ToString() != "")
                        {
                            try
                            {
                                ExpDate = item.ExpireDate.Value.ToString("dd/MM/yyyy");
                            }
                            catch (Exception)
                            { }
                        }
                        else
                        {
                            try
                            {
                                ExpDate = item.ExpireDate.Value.ToString("dd/MM/yyyy");
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    decimal prqty = 0;
                    try
                    {
                        PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                        var PurDData = PurRDRepo.GetAll().Where(t => t.POID == item.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        prqty = Convert.ToDecimal(PurDData.Quantity);
                    }
                    catch (Exception)
                    {
                        prqty = 0;
                    }
                    if (i > 0)
                    {
                        decimal srqty = 0;
                        for (int l = 0; l < dtgvBatchInfo.Rows.Count; l++)
                        {
                            if (ExpDate == "")
                            {
                                if (Convert.ToString(dtgvBatchInfo.Rows[l].Cells["BatchNo"].Value) == item.BatchNo && Convert.ToInt32(dtgvBatchInfo.Rows[l].Cells["POrderNo"].Value) == item.PorderNo)
                                    goto jump;
                                else
                                    goto endloop;
                            }
                            else if (Convert.ToString(dtgvBatchInfo.Rows[l].Cells["BatchNo"].Value) == item.BatchNo && dtgvBatchInfo.Rows[l].Cells["Expiryg"].Value.ToString() == ExpDate && Convert.ToInt32(dtgvBatchInfo.Rows[l].Cells["POrderNo"].Value) == item.PorderNo)
                                goto jump;
                            else
                                goto endloop;
                        jump:
                            {
                                dtgvBatchInfo.Rows[l].Cells["Quantityg"].Value = Convert.ToDecimal(dtgvBatchInfo.Rows[l].Cells["Quantityg"].Value) + (Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty);
                                try
                                {
                                    bRepo = new BatchRepository();
                                    srqty = 0;
                                    decimal UsedQty = 0;
                                    if (ExpDate == "")
                                        UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                    else
                                        UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                    try
                                    {
                                        BatchRepository batchrpstr = new BatchRepository();
                                        List<Batch> BData = new List<Batch>();
                                        if (ExpDate == "")
                                            BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                        else
                                            BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                        if (BData != null)
                                        {
                                            foreach (var item1 in BData)
                                            {
                                                try
                                                {
                                                    GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                    GoodReturnDetail SRData = new GoodReturnDetail();
                                                    SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    if (SRData != null)
                                                        srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                    SRRepo.Dispose();
                                                }
                                                catch (Exception)
                                                { }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                    UsedQty = UsedQty - srqty;
                                    dtgvBatchInfo.Rows[l].Cells["UsedQtyg"].Value = UsedQty;
                                    dtgvBatchInfo.Rows[l].Cells["RemQtyg"].Value = Convert.ToDecimal(dtgvBatchInfo.Rows[l].Cells["Quantityg"].Value) + (Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty) - UsedQty;
                                }
                                catch (Exception)
                                { }
                                m = 1;
                                break;
                            }
                        endloop: { }
                        }
                        if (m == 0)
                        {                            
                            try
                            {
                                bRepo = new BatchRepository();
                                decimal UsedQty = 0;
                                if (ExpDate == "")
                                    UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                else
                                    UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                decimal srqty1 = 0;
                                try
                                {
                                    BatchRepository batchrpstr = new BatchRepository();
                                    List<Batch> BData = new List<Batch>();
                                    if (ExpDate == "")
                                        BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                    else
                                        BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                    if (BData != null)
                                    {
                                        foreach (var item1 in BData)
                                        {
                                            try
                                            {
                                                GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                GoodReturnDetail SRData = new GoodReturnDetail();
                                                SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                srqty1 = srqty1 + Convert.ToDecimal(SRData.Quantity);
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    srqty1 = 0;
                                }
                                if (((Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty) - (UsedQty - srqty1)) != 0)
                                {
                                    dtgvBatchInfo.Rows.Add();
                                    dtgvBatchInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                                    dtgvBatchInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                                    dtgvBatchInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                                    if (ExpDate == "")
                                        dtgvBatchInfo.Rows[i].Cells["Expiryg"].Value = "";
                                    else
                                        dtgvBatchInfo.Rows[i].Cells["Expiryg"].Value = item.ExpireDate.Value.ToString("dd/MM/yyyy");
                                    dtgvBatchInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                    dtgvBatchInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty;
                                    dtgvBatchInfo.Rows[i].Cells["UsedQtyg"].Value = UsedQty - srqty1;
                                    dtgvBatchInfo.Rows[i].Cells["RemQtyg"].Value = (Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty) - (UsedQty - srqty1);
                                    i++;
                                }
                            }
                            catch (Exception)
                            { }                            
                        }
                    }
                    else
                    {
                        try
                        {
                            bRepo = new BatchRepository();
                            decimal UsedQty = 0;
                            if (ExpDate == "")
                                UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                            else
                                UsedQty = Convert.ToDecimal(bRepo.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                            decimal srqty = 0;
                            try
                            {
                                BatchRepository batchrpstr = new BatchRepository();
                                List<Batch> BData = new List<Batch>();
                                if (ExpDate == "")
                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                else
                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == p && t.ManufactureCompany == c && t.Unit == u && t.BatchNo == item.BatchNo && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == item.ExpireDate.Value.ToString("dd/MM/yyyy") && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                if (BData != null)
                                {
                                    foreach (var item1 in BData)
                                    {
                                        try
                                        {
                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                            srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                srqty = 0;
                            }
                            if (((Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty) - (UsedQty - srqty)) != 0)
                            {
                                dtgvBatchInfo.Rows.Add();
                                dtgvBatchInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                                dtgvBatchInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                                dtgvBatchInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                                if (ExpDate == "")
                                    dtgvBatchInfo.Rows[i].Cells["Expiryg"].Value = "";
                                else
                                    dtgvBatchInfo.Rows[i].Cells["Expiryg"].Value = item.ExpireDate.Value.ToString("dd/MM/yyyy");
                                dtgvBatchInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                dtgvBatchInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty;
                                dtgvBatchInfo.Rows[i].Cells["UsedQtyg"].Value = UsedQty - srqty;
                                dtgvBatchInfo.Rows[i].Cells["RemQtyg"].Value = (Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty) - prqty) - (UsedQty - srqty);
                                i++;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    m = 0;
                }
                pDetailRepo.Dispose(); bRepo.Dispose();
            }
            catch (Exception)
            { }
            //end of fillgrid with batchwise quantity
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string[] bexp = { "0" };
                if (dtgvBatchInfo.Columns[e.ColumnIndex].HeaderText == "")
                {
                    if (Convert.ToDecimal(dtgvBatchInfo.Rows[e.RowIndex].Cells["RemQtyg"].Value.ToString()) > 0)
                    {
                        PurchaseDetailRepository pDeailRepo = new PurchaseDetailRepository();
                        BatchRepository batchRepo = new BatchRepository();
                        string a = dtgvBatchInfo.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString();
                        string b = Convert.ToString(dtgvBatchInfo.Rows[e.RowIndex].Cells["Expiryg"].Value).Trim();
                        if (b.Contains(' '))
                            bexp = b.Split(' ');
                        if (batch == null && expiry == null)
                        {
                            batch = a;
                            expiry = b;
                            decimal netQty = 0;
                            if (expiry != "")
                            {
                                if (Convert.ToDateTime(expiry) < Convert.ToDateTime(System.DateTime.Now.Date.ToString("dd/MM/yyyy")))
                                {
                                    if ((MessageBox.Show("Product is Expired. Are you sure to generate bill with expired Product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                        goto x;
                                    else
                                        goto ends;
                                }
                            }
                        //else
                        //{
                        x:
                            {
                                List<PurchaseDetail> purlist = new List<PurchaseDetail>();
                                if (expiry == "")
                                    purlist = pDeailRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == unit && t.BatchNo == batch && t.Status == "Active" && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                                else
                                    purlist = pDeailRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == unit && t.BatchNo == batch && t.ExpireDate.Value.ToString("dd/MM/yyyy") == expiry && t.Status == "Active" && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();

                                if (purlist != null)
                                {
                                    decimal puqty = 0, frqty = 0, bQty = 0, prqty = 0, srqty = 0;
                                    foreach (var i in purlist)
                                    {
                                        BatchRepository batchRepo1 = new BatchRepository();
                                        if (expiry == "")
                                            bQty = bQty + Convert.ToDecimal(batchRepo1.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == batch && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                        else
                                            bQty = bQty + Convert.ToDecimal(batchRepo1.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == batch && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == expiry && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                        try
                                        {
                                            BatchRepository batchrpstr = new BatchRepository();
                                            List<Batch> BData = new List<Batch>();
                                            if (expiry == "")
                                                BData = batchrpstr.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == batch && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                            else
                                                BData = batchrpstr.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == batch && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == expiry && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                            if (BData != null)
                                            {
                                                foreach (var item1 in BData)
                                                {
                                                    try
                                                    {
                                                        GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                        GoodReturnDetail SRData = new GoodReturnDetail();
                                                        SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                        srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                    }
                                                    catch (Exception)
                                                    { }
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        { }
                                        puqty = puqty + Convert.ToDecimal(i.Quantity);
                                        frqty = frqty + Convert.ToDecimal(i.FreeQty);
                                        try
                                        {
                                            PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                                            var PurDData = PurRDRepo.GetAll().Where(t => t.POID == i.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (PurDData != null)
                                                prqty = Convert.ToDecimal(PurDData.Quantity);
                                            else
                                                prqty = 0;
                                        }
                                        catch (Exception)
                                        {
                                            prqty = 0;
                                        }
                                        netQty = (puqty + frqty - prqty) - (bQty - srqty);
                                        if (pono[0] == null)
                                            pono[0] = Convert.ToString(i.PorderNo);
                                        else
                                            pono[0] = pono[0].ToString() + "," + Convert.ToString(i.PorderNo);
                                        if (Quantity <= netQty)
                                            break;
                                        batchRepo1.Dispose();
                                    }
                                }
                                if (Quantity <= netQty)
                                {
                                    MessageBox.Show("You select this Batch No " + batch, "Success");
                                    this.Close();
                                }
                                if (Quantity > netQty)
                                {
                                    remqty = Quantity - netQty;
                                    MessageBox.Show("You select this Batch No " + batch + "" + Environment.NewLine + " You have to select another Batch No.", "Warning");
                                    btnClose.Enabled = false;
                                }
                            }
                        //}
                    ends:
                        {
                            batch = null;
                            expiry = null;
                        }
                        }
                        else
                        {
                            decimal bQty = 0;
                            batch = batch + "," + a;
                            expiry = expiry + "," + bexp[0].ToString();
                            string[] bArray = batch.Split(',');
                            String[] expArray = expiry.Split(',');

                            if (bArray[k - 1] == bArray[k])
                            {
                                bArray[k] = null;
                                expArray[k] = null;
                                if (bArray.Length == 2)
                                {
                                    batch = bArray[k - 1];
                                    expiry = expArray[k - 1];
                                }
                                else
                                {
                                    for (int q = 0; q < bArray.Length; q++)
                                    {
                                        if (q == 0)
                                        {
                                            batch = bArray[0];
                                            expiry = expArray[0];
                                        }
                                        else
                                        {
                                            batch = batch + "," + bArray[q];
                                            expiry = expiry + "," + expArray[q];
                                        }
                                    }
                                }
                                MessageBox.Show("You alraedy select this Batch Nos " + bArray[k - 1], "Warning");
                            }
                            else
                            {
                                decimal netQty = 0;
                                if (Convert.ToDateTime(expArray[k]) < Convert.ToDateTime(System.DateTime.Now.Date.ToString("dd/MM/yyyy")))
                                {
                                    if ((MessageBox.Show("Product is Expired. Are you sure to generate bill with expired Product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                        goto x;
                                    else
                                        goto ends;
                                }
                            x:
                                {
                                    List<PurchaseDetail> purlist = new List<PurchaseDetail>();
                                    if (expArray[k] == "")
                                        purlist = pDeailRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.Status == "Active" && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                                    else
                                        purlist = pDeailRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.ExpireDate.Value.ToString("dd/MM/yyyy") == expArray[k] && t.Status == "Active" && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                                    if (purlist != null)
                                    {
                                        decimal puqty = 0, frqty = 0, prqty = 0, srqty = 0;
                                        foreach (var i in purlist)
                                        {
                                            if (expArray[k] == "")
                                                bQty = bQty + Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                            else
                                                bQty = bQty + Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == expArray[k] && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                            try
                                            {
                                                BatchRepository batchrpstr = new BatchRepository();
                                                List<Batch> BData = new List<Batch>();
                                                if (expArray[k] == "")
                                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                                else
                                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == unit && t.BatchNo == bArray[k] && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == expArray[k] && t.Status == "1" && t.PorderNo == i.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                                                if (BData != null)
                                                {
                                                    foreach (var item1 in BData)
                                                    {
                                                        try
                                                        {
                                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                            srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                        }
                                                        catch (Exception)
                                                        { }
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            puqty = puqty + Convert.ToDecimal(i.Quantity);
                                            frqty = frqty + Convert.ToDecimal(i.FreeQty);
                                            try
                                            {
                                                PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                                                var PurDData = PurRDRepo.GetAll().Where(t => t.POID == i.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                prqty = Convert.ToDecimal(PurDData.Quantity);
                                            }
                                            catch (Exception)
                                            {
                                                prqty = 0;
                                            }

                                            netQty = (puqty + frqty - prqty) - (bQty - srqty);
                                            if (pono[k] == null)
                                                pono[k] = Convert.ToString(i.PorderNo);
                                            else
                                                pono[k] = pono[k].ToString() + "," + Convert.ToString(i.PorderNo);
                                            if (remqty <= netQty)
                                                break;
                                        }
                                    }
                                    if (bArray.Length <= 3)
                                    {
                                        if (remqty <= netQty)
                                        {
                                            MessageBox.Show("You select this Batch Nos " + batch, "Success");
                                            this.Close();
                                        }
                                        if (remqty > netQty)
                                        {
                                            remqty = remqty - netQty;
                                            MessageBox.Show("You select this Batch Nos " + batch + "" + Environment.NewLine + " But You have select another Batch No.", "Warning");
                                        }
                                        else
                                            btnClose.Enabled = true;
                                        k++;
                                    }
                                    else
                                    {
                                        bArray[3] = null;
                                        MessageBox.Show("You Can't select more than 3 Batch Nos " + batch, "Warning");
                                    }
                                }
                            ends: { }
                            }
                        }
                        pDeailRepo.Dispose(); batchRepo.Dispose();
                    }
                    else
                        MessageBox.Show("This batch quantity is not available.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            try
            {
                frmCustomerBill.custbillinfo.batchData(batch, expiry, pono);
            }
            catch (Exception)
            { }
        }

        private void frmBatchData_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmCustomerBill.custbillinfo.batchData(batch, expiry, pono);
            }
            catch (Exception)
            { }
        }

        private void frmBatchData_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmCustomerBill.custbillinfo = null;
            }
            catch (Exception)
            { }
        }
    }
}