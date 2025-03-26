using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OrderManager
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> Items { get; set; } = new ObservableCollection<Product>();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                fillComboBox();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("An error occurred during initialization: " + e.Message);
            }
            lblTitle.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblTitle.VerticalContentAlignment = VerticalAlignment.Center;
            lblBasket2.Visibility = Visibility.Collapsed;
            lblProduct.Visibility = Visibility.Collapsed;
            cmbProducts.Visibility = Visibility.Collapsed;
            lblQuantity.Visibility = Visibility.Collapsed;
            cmbBasket1.Visibility = Visibility.Collapsed;
            txtQuantity.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            lblTitle.Background = new SolidColorBrush(Color.FromRgb(100, 161, 162));
            lodbtn.Foreground = new SolidColorBrush(Color.FromRgb(172, 190, 157));
            lodbtn.Background = new SolidColorBrush(Color.FromRgb(105, 152, 242));
            aitobtn.Background = new SolidColorBrush(Color.FromRgb(176, 220, 233));
            aitobtn.Foreground = new SolidColorBrush(Color.FromRgb(210, 220, 160));
            exitbtn.Background = new SolidColorBrush(Color.FromRgb(105, 152, 242));
            exitbtn.Foreground = new SolidColorBrush(Color.FromRgb(172, 190, 157));
            btnCancel.Foreground = new SolidColorBrush(Color.FromRgb(172, 190, 157));
            btnCancel.Background = new SolidColorBrush(Color.FromRgb(105, 152, 242));
            btnSave.Foreground = new SolidColorBrush(Color.FromRgb(172, 190, 157));
            btnSave.Background = new SolidColorBrush(Color.FromRgb(105, 152, 242));
            SetupDataGridAndHandlers();
        }

        public void fillComboBox()
        {
            try
            {
                using (var conn = new Connection())
                {
                    var basket = conn.baskets.Join(
                            conn.shoppers,
                            shopper => shopper.IdShopper,
                            basket => basket.IdShopper,
                            (basket,shopper) => new { 
                                Output = $"{shopper.Email} {basket.IdBasket}"
                            }
                    ).Select(result => result.Output).ToList();
                    cmbBasket.ItemsSource = basket;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error in fillComboBox: " + e.Message);
            }
        }

        // Initialize DataGrid only once during application setup
        private void InitializeDataGrid()
        {
            // Disable auto-generation of columns to avoid duplicates
            dgBaskets.AutoGenerateColumns = false;

            // Define the columns
            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Basket Id",
                Binding = new Binding("BasketId")
            });

            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Basket Item Id",
                Binding = new Binding("BasketItemId")
            });

            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Product Id",
                Binding = new Binding("ProductId")
            });

            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Product Name",
                Binding = new Binding("ProductName")
            });

            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Unit Price",
                Binding = new Binding("UnitPrice")
            });

            dgBaskets.Columns.Add(new DataGridTextColumn
            {
                Header = "Quantity",
                Binding = new Binding("Quantity")
            });
        }

        // Update DataGrid dynamically based on basket selection
        private void cmbBasket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selected = cmbBasket.SelectedItem?.ToString();
            string[]? parts = selected?.Split(' ');

            // Validate input
            if (parts == null || parts.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine("Invalid selection!");
                return;
            }

            try
            {
                using (var conn = new Connection())
                {
                    // Fetch the basket items with relevant product details
                    var basketItems = conn.bitms.Join(
                        conn.products,
                        bitms => bitms.IdProduct,
                        product => product.IdProduct,
                        (bitms, product) => new
                        {
                            BasketId = bitms.IdBasket,
                            BasketItemId = bitms.IdBasketItem,
                            ProductId = bitms.IdProduct,
                            ProductName = product.ProductName,
                            UnitPrice = product.Price,
                            Quantity = bitms.Quantity
                        }
                    )
                    .Where(item => item.BasketId == Convert.ToInt32(parts[1]))
                    .ToList();

                    // Bind the fetched data to the DataGrid
                    dgBaskets.ItemsSource = basketItems;
                }
            }
            catch (Exception ex)
            {
                // Handle and log exceptions
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        // Call this method once (e.g., during form initialization or window loading)
        private void SetupDataGridAndHandlers()
        {
            InitializeDataGrid();
            cmbBasket.SelectionChanged += cmbBasket_SelectionChanged;
        }

        private void aitobtn_Click(object sender, RoutedEventArgs e)
        {
            lblBasket.Visibility = Visibility.Collapsed;
            cmbBasket.Visibility = Visibility.Collapsed;
            dgBaskets.Visibility = Visibility.Collapsed;
            lblBasket2.Visibility = Visibility.Visible;
            lblProduct.Visibility = Visibility.Visible;
            cmbProducts.Visibility = Visibility.Visible;
            lblQuantity.Visibility = Visibility.Visible;
            cmbBasket1.Visibility = Visibility.Visible;
            txtQuantity.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            txtQuantity.Text = string.Empty;
            cmbBasket1.ItemsSource = cmbBasket.Items;
            fillProductcmb();
        }

        private void fillProductcmb() {
            try {
                using (var conn = new Connection())
                {
                    var results = conn.products
                                      .Select(products => products.ProductName) // Select only the ProductName as a string
                                      .ToList();

                    cmbProducts.ItemsSource = results; // Bind directly to the ComboBox
                }
            } catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string? bsktId = cmbBasket1.SelectedItem?.ToString();
            string? productName = cmbProducts.SelectedItem?.ToString();
            string[]? id = bsktId?.Split(' ');

            if (id == null || id.Length < 2 || string.IsNullOrEmpty(productName))
            {
                System.Diagnostics.Debug.WriteLine("Invalid input data!");
                return;
            }

            if (string.IsNullOrEmpty(txtQuantity.Text) || !byte.TryParse(txtQuantity.Text, out byte newQuantity))
            {
                System.Diagnostics.Debug.WriteLine("Invalid quantity input!");
                return;
            }

            try
            {
                using (var conn = new Connection())
                {
                    // Retrieve Product ID
                    short productId = conn.products
                                          .Where(product => product.ProductName == productName)
                                          .Select(product => product.IdProduct)
                                          .FirstOrDefault();

                    // Validate Product ID
                    if (productId == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Product not found!");
                        return;
                    }

                    // Retrieve Basket
                    var basket = conn.baskets
                                     .Where(basketId => basketId.IdBasket == Convert.ToInt32(id[1]))
                                     .FirstOrDefault();
                    if (basket == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Basket with ID {id[1]} not found!");
                        return;
                    }

                    // Check if BasketItem already exists
                    var existingBasketItem = conn.bitms
                                                  .FirstOrDefault(bi => bi.IdBasket == Convert.ToInt32(id[1]) && bi.IdProduct == productId);

                    if (existingBasketItem != null)
                    {
                        // Update Quantity of Existing BasketItem
                        existingBasketItem.Quantity += newQuantity;
                        System.Diagnostics.Debug.WriteLine($"Updated existing BasketItem. New Quantity: {existingBasketItem.Quantity}");
                    }
                    else
                    {
                        // Add New BasketItem
                        var newBasketItem = new BasketItem
                        {
                            IdBasketItem = conn.bitms.Max(bi => bi.IdBasketItem) + 1, // Generate new ID
                            IdBasket = Convert.ToInt32(id[1]),
                            IdProduct = productId,
                            Quantity = newQuantity
                        };
                        conn.bitms.Add(newBasketItem);
                        System.Diagnostics.Debug.WriteLine($"Added new BasketItem: Product ID {productId}, Quantity {newQuantity}");
                    }

                    // Update Basket
                    basket.Quantity += newQuantity;
                    var productPrice = conn.products
                                           .Where(product => product.IdProduct == productId)
                                           .Select(product => product.Price)
                                           .FirstOrDefault();
                    basket.SubTotal += productPrice * newQuantity;

                    conn.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Basket and basket item updated successfully!");

                    // Refresh DataGrid
                    RefreshDataGrid();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to cancel?";
            string title = "Cancel";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage image = MessageBoxImage.Information;
            MessageBoxResult result;
            result = MessageBox.Show(message, title, button, image);
            if (result == MessageBoxResult.Yes)
            {
                lblBasket2.Visibility = Visibility.Collapsed;
                lblProduct.Visibility = Visibility.Collapsed;
                cmbProducts.Visibility = Visibility.Collapsed;
                lblQuantity.Visibility = Visibility.Collapsed;
                cmbBasket1.Visibility = Visibility.Collapsed;
                txtQuantity.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                btnCancel.Visibility = Visibility.Collapsed;
                dgBaskets.Visibility = Visibility.Visible;
                lblBasket.Visibility = Visibility.Visible;
                cmbBasket.Visibility = Visibility.Visible;
            }
        }

        private void RefreshDataGrid()
        {
            string? selected = cmbBasket1.SelectedItem?.ToString();
            string[]? parts = selected?.Split(' ');

            if (parts == null || parts.Length < 2)
            {
                System.Diagnostics.Debug.WriteLine("Invalid basket selection!");
                return;
            }

            try
            {
                using (var conn = new Connection())
                {
                    var basketItems = conn.bitms.Join(
                        conn.products,
                        bitms => bitms.IdProduct,
                        product => product.IdProduct,
                        (bitms, product) => new
                        {
                            BasketId = bitms.IdBasket,
                            BasketItemId = bitms.IdBasketItem,
                            ProductId = bitms.IdProduct,
                            ProductName = product.ProductName,
                            UnitPrice = product.Price,
                            Quantity = bitms.Quantity
                        }
                    )
                    .Where(item => item.BasketId == Convert.ToInt32(parts[1]))
                    .ToList();

                    // Update DataGrid binding
                    dgBaskets.ItemsSource = basketItems;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing DataGrid: {ex.Message}");
            }
        }

        private void lodbtn_Click(object sender, RoutedEventArgs e)
        {
            lblBasket2.Visibility = Visibility.Collapsed;
            lblProduct.Visibility = Visibility.Collapsed;
            cmbProducts.Visibility = Visibility.Collapsed;
            lblQuantity.Visibility = Visibility.Collapsed;
            cmbBasket1.Visibility = Visibility.Collapsed;
            txtQuantity.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnCancel.Visibility = Visibility.Collapsed;
            dgBaskets.Visibility = Visibility.Visible;
            lblBasket.Visibility = Visibility.Visible;
            cmbBasket.Visibility = Visibility.Visible;
            RefreshDataGrid();
        }

        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}