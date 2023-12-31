﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Select * From TypeOfCars
            //var cars = _db.TypesOfCars.ToList();

            //Select Id as CarId, name as CarName from TypesOfCars

            // var cars = _db.TypesOfCars.Select(q => new { CarId = q.Id, CarName = q.Make}).ToList();


            var cars = _db.TypesOfCars
                .Select(q => new
                {

                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "Licene Plate Number";
            gvVehicleList.Columns[5].Visible = false;
            //gvVehicleList.Columns[0].HeaderText = "ID";
            //gvVehicleList.Columns[1].HeaderText = "Name";
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    //delete vehicle from table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void btnEditCar_Click(object sender, EventArgs e)
        {
            // get Id of selected row
            var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

            // query databse for record

            var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);



            //launch AddEditVehicle window with data
             
            var addEditVehicle = new AddEditVehicle(car, this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();


        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            {
                //Simple Refresh Option
                PopulateGrid();
            }
        }

        //New Function to PopulateGrid. Can be called anytime we need a grid refresh

        public void PopulateGrid()
        {
            // Select a customer model collection of cars from database
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from the hard coded column value to the name,

            // to make it more dynamic.
            gvVehicleList.Columns["ID"].Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

