using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MTVaniKhanna
{

    public partial class MainWindow : Window
    {
        List<Vehicle> vehicles;

        public MainWindow()
        {
            InitializeComponent();

            vehicles = new List<Vehicle>()
            {
               new Car(1, "Toyota Camry", 50.00, "Sedan", "Standard", true),
               new Car(2, "Honda Civic", 60.00, "Sedan", "Exotic", true),
               new Car(3, "Ford Explorer", 69.99, "SUV", "Exotic", true),
               new Car(4, "Nisan Versa", 50.00, "Hatchback", "Standard", false),
               new Car(5, "Hundayi Tucsan", 89.90, "SUV", "Standard", false),
               new Car(6, "McLaern P1", 199.90, "Sports", "Exotic", false),
             new Motorcycle(7, "Suzuki Boulevard", 49.90, "Cruiser", "Bike", false),
              new Motorcycle(8, "Harley Davidson", 79.90, "Cruiser", "Bike", false),
              new Motorcycle(9, "Honda CRF125", 39.90, "Dirt", "Bike", false),
              new Motorcycle(10, "Durati Monster", 69.90, "Sports", "Bike", true),
              new Motorcycle(11, "Can-Am spyder", 59.90, "Cruiser", "Trike", true),
              new Motorcycle(12, "Polaris Slingshot", 69.90, "Cruiser", "Trike", true),
            };

            RefreshList();
            txtID.IsReadOnly = true;

            btnEdit.IsEnabled = false;
            btnDel.IsEnabled = false;
        }
        private void RefreshList()
        {
            listBox.ItemsSource = vehicles.Select(v => v.Name);

            List<string> categories = Enum.GetNames(typeof(Category)).ToList();
            comboBoxCategory.ItemsSource = categories;

            List<string> types = Enum.GetNames(typeof(Type)).ToList();
            comboBoxType.ItemsSource = types;
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                string selectedName = listBox.SelectedItem.ToString();
                Vehicle selectedVehicle = null;

                foreach (Vehicle vehicle in vehicles)
                {
                    if (vehicle.Name == selectedName)
                    {
                        selectedVehicle = vehicle;
                        break;
                    }
                }
                if (selectedVehicle != null)
                {
                    // Set appropriate RadioButton
                    if (selectedVehicle is Car)
                    {
                        rdCar.IsChecked = true;
                    }
                    else if (selectedVehicle is Motorcycle)
                    {
                        rdBike.IsChecked = true;
                    }

                    txtID.Text = selectedVehicle.ID.ToString();
                    txtname.Text = selectedVehicle.Name;
                    txtRentalPrice.Text = selectedVehicle.RentalPrice.ToString();

                    comboBoxCategory.SelectedItem = selectedVehicle.Category;
                    comboBoxType.SelectedItem = selectedVehicle.Type;

                    IsReserved.IsChecked = selectedVehicle.IsReserved; 


                    btnEdit.IsEnabled = true;
                    btnDel.IsEnabled = true;

                }
            }
            else
            {
                ClearControls(); 
            }
        }
        private void ClearControls()
        {
            rdCar.IsChecked = false;
            rdBike.IsChecked = false;
            txtID.Text = string.Empty;
            txtname.Text = string.Empty;
            txtRentalPrice.Text = string.Empty;
            comboBoxCategory.SelectedItem = null;
            comboBoxType.SelectedItem = null;
            IsReserved.IsChecked = false;
            btnEdit.IsEnabled = false;
            btnDel.IsEnabled = false;
        }

        private void ValidateInputs()
        {
            if (rdCar.IsChecked == false && rdBike.IsChecked == false)
            {
                throw new Exception("Please select Car or Motorcycle.");
            }

         
            if (string.IsNullOrEmpty(txtname.Text))
            {
                throw new Exception("Name field can not be left blank.");
            }

            if (!double.TryParse(txtRentalPrice.Text, out _))
            {
                throw new Exception("Please enter a valid Rental Price.");
            }

            if (comboBoxCategory.SelectedItem == null )
            {
                throw new Exception("Please select Category ");
            }
            if (comboBoxType.SelectedItem == null)
            {
                throw new Exception("Please Select type!");
            }
            if (!txtname.Text.All(char.IsLetter))
            {
                throw new Exception("Name field can only contain letters.");
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateInputs();
                int newId = vehicles.Last().ID + 1;
                string type = rdCar.IsChecked == true ? "Car" : "Motorcycle";
                bool isReservedValue = IsReserved.IsChecked == true;



                Vehicle newVehicleAdded;

                if (type == "Car")
                {
                    newVehicleAdded = new Car(newId, txtname.Text, Convert.ToDouble(txtRentalPrice.Text), comboBoxCategory.SelectedItem.ToString(), comboBoxType.SelectedItem.ToString(), isReservedValue);
                }
                else
                {
                    newVehicleAdded = new Motorcycle(newId, txtname.Text, Convert.ToDouble(txtRentalPrice.Text), comboBoxCategory.SelectedItem.ToString(), comboBoxType.SelectedItem.ToString(), isReservedValue);
                }
                vehicles.Add(newVehicleAdded);
                RefreshList();

                ClearControls();

                MessageBox.Show("Vehicle added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*if (rdCar.IsChecked == false && rdBike.IsChecked == false)
        {
            MessageBox.Show("Please select Car or Motorcycle.");
            return;
        }
        if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtRentalPrice.Text))
        {
            MessageBox.Show("Please fill in Name and Rental Price.");
            return;
        }
        if (comboBoxCategory.SelectedItem == null || comboBoxType.SelectedItem == null)
        {
            MessageBox.Show("Please select Category and Type.");
            return;
        }

        if (rdCar.IsChecked == true)
        {
            type = "Car";
        }
        else
        {
            type = "Motorcycle";
        }

        string isReserved = IsReserved.IsChecked == true ? "yes" : "no";

        Vehicle newVehicle;

        if (type == "Car")
        {
            newVehicle = new Car(newId, txtname.Text, Convert.ToDouble(txtRentalPrice.Text), comboBoxCategory.SelectedItem.ToString(), comboBoxType.SelectedItem.ToString(), isReserved);
        }
        else
        {
            newVehicle = new Motorcycle(newId, txtname.Text, Convert.ToDouble(txtRentalPrice.Text), comboBoxCategory.SelectedItem.ToString(), comboBoxType.SelectedItem.ToString(), isReserved);
        }

        vehicles.Add(newVehicle);
        RefreshList();

        // Clear controls
        ClearControls();

        MessageBox.Show("Vehicle added successfully.");
    }
        */

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            /* if (listBox.SelectedItem != null)
             {
                 string selectedName = listBox.SelectedItem.ToString();
                 Vehicle selectedVehicle = null;

                 foreach (Vehicle vehicle in vehicles)
                 {
                     if (vehicle.Name == selectedName)
                     {
                         selectedVehicle = vehicle;
                         break;
                     }
                 }

                 if (selectedVehicle != null)
                 {
                     // Check constraints before editing
                     if (rdCar.IsChecked == false && rdBike.IsChecked == false)
                     {
                         MessageBox.Show("Please select Car or Motorcycle.");
                         return;
                     }

                     if (string.IsNullOrEmpty(txtname.Text) )
                     {
                         MessageBox.Show("Please fill in Name ");
                         return;
                     }
                     if (string.IsNullOrEmpty(txtRentalPrice.Text))
                     {
                         MessageBox.Show("Please fill Rental Price.");
                         return;
                     }

                     if (comboBoxCategory.SelectedItem == null || comboBoxType.SelectedItem == null)
                     {
                         MessageBox.Show("Please select Category and Type.");
                         return;
                     }
                     selectedVehicle.Name = txtname.Text;
                     selectedVehicle.RentalPrice = Convert.ToDouble(txtRentalPrice.Text);
                     selectedVehicle.Category = comboBoxCategory.SelectedItem.ToString();
                     selectedVehicle.Type = comboBoxType.SelectedItem.ToString();
                     selectedVehicle.IsReserved = IsReserved.IsChecked == true ? "yes" : "no";

                     RefreshList();
                     MessageBox.Show("Vehicle edited successfully.");
                 }
             }
             else
             {
                 MessageBox.Show("Please select a vehicle from the list.");
             }*/

            //edit begins here

            try
            {
                if (listBox.SelectedItem != null)
                {
                    string selectedName = listBox.SelectedItem.ToString();
                    Vehicle selectedVehicle = null;

                    foreach (Vehicle vehicle in vehicles)
                    {
                        if (vehicle.Name == selectedName)
                        {
                            selectedVehicle = vehicle;
                            break;
                        }
                    }

                    if (selectedVehicle != null)
                    {
                        ValidateInputs();
                      /*  foreach (char c in txtname.Text)
                        {
                            if (!char.IsLetter(c))
                            {
                                throw new Exception("Name field can only contain letters.");
                            }
                        }*/

                        selectedVehicle.Name = txtname.Text;
                        selectedVehicle.RentalPrice = Convert.ToDouble(txtRentalPrice.Text);
                        selectedVehicle.Category = comboBoxCategory.SelectedItem.ToString();
                        selectedVehicle.Type = comboBoxType.SelectedItem.ToString();
                        selectedVehicle.IsReserved = IsReserved.IsChecked == true;

                        RefreshList();
                        MessageBox.Show("Vehicle edited successfully.");
                    }
                }
                else
                {
                   MessageBox.Show("Please select a vehicle from the list.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
          
            Vehicle vh = vehicles[listBox.SelectedIndex];
             vehicles.Remove(vh);
              RefreshList();
            MessageBox.Show("Vehile Removed Sucessfully");

        }

        private void btnClear1_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listBox.ItemsSource = null;
            listBox.Items.Clear();
            ClearControls();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            vehicles = new List<Vehicle>()
    {
       new Car(1, "Toyota Camry", 50.00, "Sedan", "Standard", true),
               new Car(2, "Honda Civic", 60.00, "Sedan", "Exotic", true),
               new Car(3, "Ford Explorer", 69.99, "SUV", "Exotic", true),
               new Car(4, "Nisan Versa", 50.00, "Hatchback", "Standard", false),
               new Car(5, "Hundayi Tucsan", 89.90, "SUV", "Standard", false),
               new Car(6, "McLaern P1", 199.90, "Sports", "Exotic", false),
             new Motorcycle(7, "Suzuki Boulevard M109R", 49.90, "Cruiser", "Bike", false),
              new Motorcycle(8, "Harley Davidson", 79.90, "Cruiser", "Bike", false),
              new Motorcycle(9, "Honda CRF125", 39.90, "Dirt", "Bike", false),
              new Motorcycle(10, "Durati Monster", 69.90, "Sports", "Bike", true),
              new Motorcycle(11, "Can-Am spyder", 59.90, "Cruiser", "Trike", true),
              new Motorcycle(12, "Polaris Slingshot", 69.90, "Cruiser", "Trike", true),
    };
            RefreshList();
            ClearControls();
        }
    }
}

        
    



