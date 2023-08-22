using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {

        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing; 
        private readonly CarRentalEntities _db = new CarRentalEntities();

        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;
            _db = new CarRentalEntities();
            
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehicleListing manageVehicleListing)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle"; 
            isEditMode = true;
            _manageVehicleListing = manageVehicleListing;
            _db = new CarRentalEntities();
            PopulateFields(carToEdit);
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Model_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
             
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if(isEditMode == true)
            if (isEditMode)
            {
                 //Edit Code here
                 var id = int.Parse(lblId.Text);
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                car.Model = tbModel.Text;
                car.Make = tbMake.Text;
                car.VIN = tbVIN.Text;
                car.Year = int.Parse(tbYear.Text);  
                car.LicensePlateNumber = tbLicenseNum.Text;

                _db.SaveChanges();

            }
            else
            {
                // Add Code Here
                var newCar = new TypesOfCar
                {
                    LicensePlateNumber = tbLicenseNum.Text,
                    Make = tbMake.Text,
                    Model = tbModel.Text,
                    VIN = tbVIN.Text,
                    Year = int.Parse(tbYear.Text)

                };

                _db.TypesOfCars.Add(newCar);
                _db.SaveChanges();
                _manageVehicleListing.PopulateGrid();
                MessageBox.Show("Insert Operation Completed. Refresh Grid To See Changes");
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
