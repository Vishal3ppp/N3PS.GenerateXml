using Microsoft.Win32;
using N3PS.GenerateXml.DataTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml;

namespace N3PS.GenerateXml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Tables fieldsTable = new Tables();
            DataTable dt = fieldsTable.GetFieldsTable();
            dgFields.ItemsSource = dt.DefaultView;


            DataTable dtValidationRules = fieldsTable.GetValidationsTable();
            dgValidationRule.ItemsSource = dtValidationRules.DefaultView;
            cbValidationType_OnDropDownClosed(null, null);
        }


        private void btnAddField_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFieldName.Text.Trim()))
            {
                MessageBox.Show("Enter the valid Field Name.");
                return;
            }


            if (string.IsNullOrEmpty(txtColumnNumber.Text.Trim()))
            {
                MessageBox.Show("Enter the Column Number.");
                return;
            }


            if (string.IsNullOrEmpty(txtSequenceNumber.Text.Trim()))
            {
                MessageBox.Show("Enter the Sequence Number.");
                return;
            }



            if (string.IsNullOrEmpty(txtLength.Text.Trim()))
            {
                MessageBox.Show("Enter the Length.");
                return;
            }


            DataView dv = dgFields.ItemsSource as DataView;
            DataTable dt = dv.Table;
            DataRow dr = dt.NewRow();
            dr["FieldName"] = txtFieldName.Text.Trim();
            dr["ColumnNumber"] = txtColumnNumber.Text.Trim();

            dr["SquenceNumber"] = txtSequenceNumber.Text.Trim();
            dr["Length"] = txtLength.Text.Trim();

            dt.Rows.Add(dr);
            dgFields.ItemsSource = dv;


            txtFieldName.Text = string.Empty;
            txtColumnNumber.Text = string.Empty;
            txtSequenceNumber.Text = string.Empty;
            txtLength.Text = string.Empty;


        }

        private void btnGridFieldsDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dgFields.SelectedItem;

            DataView dv = dgFields.ItemsSource as DataView;
            DataTable dt = dv.Table;


            dt.Rows.Remove(row.Row);
        }



        private void btnOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtFlatFilePath.Text = openFileDialog.FileName;
            }
        }



        private void btnGenerateXML_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtRecordSize.Text.Trim()))
            {
                MessageBox.Show("Record Size is not provided.");
                return;
            }



            if (string.IsNullOrEmpty(txtFlatFilePath.Text.Trim()))
            {
                MessageBox.Show("Flat File Path is not provided.");
                return;
            }
            StringBuilder buildXml = new StringBuilder();
            buildXml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            buildXml.AppendLine("<FlatFileFormat>");
            string headerExist = (chkHeaderExist.IsChecked == true ? "Yes" : "No");
            buildXml.AppendLine($"<HeaderExist>{headerExist }</HeaderExist>");
            buildXml.AppendLine($"<RecordSize>{txtRecordSize.Text.Trim()}</RecordSize>");
            buildXml.AppendLine($"<FlatFilePath>{txtFlatFilePath.Text.Trim()}</FlatFilePath>");

            buildXml.AppendLine("<Fields>");
            DataView dv = dgFields.ItemsSource as DataView;
            DataTable dt = dv.Table;
            foreach (DataRow dr in dt.Rows)
            {
                buildXml.AppendLine($"<Field Name=\"{dr["FieldName"].ToString().Trim()}\" ColumnNumber=\"{dr["ColumnNumber"].ToString().Trim()}\">");
                buildXml.AppendLine($"<StartPos>{dr["SquenceNumber"].ToString().Trim()}</StartPos>");
                buildXml.AppendLine($"<Length>{dr["Length"].ToString().Trim()}</Length>");
                buildXml.AppendLine("</Field>");

            }
            buildXml.AppendLine("</Fields>");
            buildXml.AppendLine("</FlatFileFormat>");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, buildXml.ToString());
            }


        }


        private void btnLoadXML_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(filePath))
                {
                    //logger.Info("Loading the Flat File Xml.");
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);



                    //logger.Info("Retrieving the HeaderExist element.");
                    XmlNode headerExistNode = xmlDoc.SelectSingleNode("//FlatFileFormat/HeaderExist");
                    if (headerExistNode != null)
                    {
                        //logger.Info("HeaderExist element is exist.");
                        chkHeaderExist.IsChecked = headerExistNode.InnerText.Trim().ToLower() == "yes" ? true : false;
                    }


                    //logger.Info("Retrieving the RecordSize element.");
                    XmlNode recordSizeNode = xmlDoc.SelectSingleNode("//FlatFileFormat/RecordSize");
                    if (recordSizeNode != null)
                    {
                        //logger.Info("RecordSize element is exist.");
                        txtRecordSize.Text = recordSizeNode.InnerText.Trim() == string.Empty ? "0" : recordSizeNode.InnerText.Trim();
                    }






                    //logger.Info("Retrieving the FlatFile element.");
                    XmlNode flatFilePathNode = xmlDoc.SelectSingleNode("//FlatFileFormat/FlatFilePath");
                    if (flatFilePathNode != null)
                    {
                        //logger.Info("FlatFile element is exist.");
                        txtFlatFilePath.Text = flatFilePathNode.InnerText.Trim();
                    }

                    //flatFile.FlatFilePath = xmlPath;


                    //logger.Info("Retrieve all fields.");
                    XmlNodeList fieldNodes = xmlDoc.SelectNodes("//FlatFileFormat/Fields/Field");


                    DataView dv = dgFields.ItemsSource as DataView;
                    DataTable dt = dv.Table;

                    dt.Rows.Clear();
                    //DataRow dr = dt.NewRow();
                    //dr["FieldName"] = txtFieldName.Text.Trim();
                    //dr["ColumnNumber"] = txtColumnNumber.Text.Trim();

                    //dr["SquenceNumber"] = txtSequenceNumber.Text.Trim();
                    //dr["Length"] = txtLength.Text.Trim();

                    //dt.Rows.Add(dr);

                    if (fieldNodes != null)
                    {
                        foreach (XmlNode fieldNode in fieldNodes)
                        {
                            //Fields field = new Fields();
                            DataRow dr = dt.NewRow();
                            if (fieldNode.Attributes["Name"] != null)
                            {

                                dr["FieldName"] = fieldNode.Attributes["Name"].Value;
                                //logger.Info("Fetched name of Field." + field.FieldName);
                            }
                            else
                            {
                                //logger.Info("Field Name is not specified so end the loop.");
                                break;
                            }


                            if (fieldNode.Attributes["ColumnNumber"] != null)
                            {

                                dr["ColumnNumber"] = fieldNode.Attributes["ColumnNumber"].Value;
                                //logger.Info("Fetched Column Number of Field." + field.ColumnNumber);
                            }
                            else
                            {
                                //logger.Info("Field Column Numbeer is not specified so end the loop.");
                                break;
                            }


                            //logger.Info("Fetching start position element.");
                            XmlNode startPosNode = fieldNode.SelectSingleNode("StartPos");
                            if (startPosNode != null)
                            {

                                dr["SquenceNumber"] = startPosNode.InnerText.Trim();
                                //logger.Info("Fetched start position element : " + field.StartPosition);
                            }
                            else
                            {
                                //logger.Info("Field Start Position is not specified so end the loop.");
                                break;
                            }




                            //logger.Info("Fetching Length element.");
                            XmlNode lengthNode = fieldNode.SelectSingleNode("Length");
                            if (lengthNode != null)
                            {

                                dr["Length"] = lengthNode.InnerText.Trim();
                                //logger.Info("Fetched Length element : " + field.Length);
                            }
                            else
                            {
                                //logger.Info("Field Start Position is not specified so end the loop.");
                                break;
                            }
                            dt.Rows.Add(dr);
                            //logger.Info("Adding field into field list.");
                            //flatFile.fields.Add(field);
                        }


                        dgFields.ItemsSource = dt.DefaultView;
                    }
                }
            }
        }

        private void btnGridValidationRulesDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dgValidationRule.SelectedItem;

            DataView dv = dgValidationRule.ItemsSource as DataView;
            DataTable dt = dv.Table;


            dt.Rows.Remove(row.Row);
        }
        private void btnAddValidationRule_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = cbValidationType.SelectedItem as ComboBoxItem;
            if (item != null)
            {
                string selectedName = item.Content.ToString().ToLower();
                switch (selectedName)
                {
                    case "length validation":
                        {
                            if (string.IsNullOrEmpty(txtValidationSize.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Size.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }


                        }
                        break;

                    case "mandatory check":
                        {



                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }


                        }
                        break;
                    case "value validation":
                        {

                            if (string.IsNullOrEmpty(txtValidationValues.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Values.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }


                        }
                        break;
                    case "number validation":
                        {
                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }
                        }
                        break;
                    case "date validation":
                        {
                            if (string.IsNullOrEmpty(txtDateFormat.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Date Formate.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }
                        }
                        break;
                    case "external routine call":
                        {
                            if (string.IsNullOrEmpty(txtValidationColumnNumber.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Column Number.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtDLLName.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the DLL Name.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtFullyQualifiedClassName.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Fully Qualified Class Name.");
                                return;
                            }


                            if (string.IsNullOrEmpty(txtRoutineName.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Routine Name.");
                                return;
                            }

                            if (string.IsNullOrEmpty(txtValidationErrorMessage.Text.Trim()))
                            {
                                MessageBox.Show("Please provide the Validation Error Message.");
                                return;
                            }
                        }
                        break;
                }




                DataView dv = dgValidationRule.ItemsSource as DataView;
                DataTable dt = dv.Table;
                DataRow dr = dt.NewRow();
                dr["ValidationId"] = txtValidationId.Text.Trim();
                dr["ValidationName"] = txtValidationName.Text.Trim();
                dr["ColumnNumber"] = txtValidationColumnNumber.Text.Trim();
                dr["ValidationType"] = (cbValidationType.SelectedItem as ComboBoxItem).Content;
                dr["ValidationSize"] = txtValidationSize.Text.Trim();
                dr["ValidationErrorMessage"] = txtValidationErrorMessage.Text.Trim();

                dr["ValidationValues"] = txtValidationValues.Text.Trim();
                dr["DateFormate"] = txtDateFormat.Text.Trim();
                dr["DLLName"] = txtDLLName.Text.Trim();
                dr["FullyQualifiedClassName"] = txtFullyQualifiedClassName.Text.Trim();
                dr["RoutineName"] = txtRoutineName.Text.Trim();

                dt.Rows.Add(dr);
                dgValidationRule.ItemsSource = dv;


                txtValidationId.Text = string.Empty;
                txtValidationName.Text = string.Empty;
                txtValidationColumnNumber.Text = string.Empty;
                cbValidationType.SelectedIndex = 0;
                txtValidationSize.Text = string.Empty;
                txtValidationErrorMessage.Text = string.Empty;

                txtValidationValues.Text = string.Empty;
                txtDateFormat.Text = string.Empty;
                txtDLLName.Text = string.Empty;
                txtFullyQualifiedClassName.Text = string.Empty;
                txtRoutineName.Text = string.Empty;

                cbValidationType_OnDropDownClosed(null, null);
            }
        }


        private void btnGenerateValidationXML_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = dgValidationRule.ItemsSource as DataView;
            DataTable dt = dv.Table;

            if (dt != null)
            {
                StringBuilder buildValidationXML = new StringBuilder();
                buildValidationXML.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                buildValidationXML.AppendLine("<ValidationRules>");
                foreach (DataRow dr in dt.Rows)
                {
                    //ComboBoxItem item = cbValidationType.SelectedItem as ComboBoxItem;
                    string validationType = dr["ValidationType"].ToString();
                    if (!string.IsNullOrEmpty(validationType))
                    {

                        buildValidationXML.AppendLine($"<ValidationRule ValidationId=\"{dr["ValidationId"].ToString()}\" ValidationName=\"{dr["ValidationName"].ToString()}\" ColumnNumber=\"{dr["ColumnNumber"].ToString()}\">");
                        //string selectedName = item.Content.ToString().ToLower();
                        switch (validationType.ToLower())
                        {
                            case "length validation":
                                {
                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<ValidationSize>{dr["ValidationSize"].ToString()}</ValidationSize>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");


                                }
                                break;

                            case "mandatory check":
                                {

                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");




                                }
                                break;
                            case "value validation":
                                {

                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<Values>");
                                    string[] values = dr["ValidationValues"].ToString().Split(',');
                                    foreach (string val in values)
                                    {
                                        buildValidationXML.AppendLine($"<value>{val.Trim()}</value>");
                                    }
                                    buildValidationXML.AppendLine($"</Values>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");


                                }
                                break;
                            case "number validation":
                                {
                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");

                                }
                                break;
                            case "date validation":
                                {
                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<DateFormat>{dr["DateFormate"].ToString()}</DateFormat>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");
                                }
                                break;
                            case "external routine call":
                                {
                                    buildValidationXML.AppendLine($"<ValidationType>{dr["ValidationType"].ToString()}</ValidationType>");
                                    buildValidationXML.AppendLine($"<DLL>");
                                    buildValidationXML.AppendLine($"<DLLName>{dr["DLLName"].ToString()}</DLLName>");
                                    buildValidationXML.AppendLine($"<FullyQualififedClassName>{dr["FullyQualifiedClassName"].ToString()}</FullyQualififedClassName>");
                                    buildValidationXML.AppendLine($"<RoutineName IsStaticMethod=\"False\" ReturnType=\"bool\" InputType=\"string\">{dr["RoutineName"].ToString()}</RoutineName>");
                                    buildValidationXML.AppendLine($"</DLL>");
                                    buildValidationXML.AppendLine($"<ErrorMessage>{dr["ValidationErrorMessage"].ToString()}</ErrorMessage>");
                                }
                                break;
                        }
                        buildValidationXML.AppendLine($"</ValidationRule>");
                    }


                }

                buildValidationXML.AppendLine($"</ValidationRules>");

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, buildValidationXML.ToString());
                }
            }



        }

        private void cbValidationType_OnDropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem item = cbValidationType.SelectedItem as ComboBoxItem;
            if (item != null)
            {
                string selectedName = item.Content.ToString().ToLower();
                switch (selectedName)
                {
                    case "length validation":
                        {
                            spValidationSize.Visibility = Visibility.Visible;
                            spValidationValues.Visibility = Visibility.Collapsed;
                            spValidationDateFormat.Visibility = Visibility.Collapsed;
                            spDLLName.Visibility = Visibility.Collapsed;
                            spFullyQualifiedClassName.Visibility = Visibility.Collapsed;
                            spRoutineName.Visibility = Visibility.Collapsed;

                        }
                        break;

                    case "mandatory check":
                        {
                            spValidationSize.Visibility = Visibility.Collapsed;
                            spValidationValues.Visibility = Visibility.Collapsed;
                            spValidationDateFormat.Visibility = Visibility.Collapsed;
                            spDLLName.Visibility = Visibility.Collapsed;
                            spFullyQualifiedClassName.Visibility = Visibility.Collapsed;
                            spRoutineName.Visibility = Visibility.Collapsed;

                        }
                        break;
                    case "value validation":
                        {
                            spValidationSize.Visibility = Visibility.Collapsed;
                            spValidationValues.Visibility = Visibility.Visible;
                            spValidationDateFormat.Visibility = Visibility.Collapsed;
                            spDLLName.Visibility = Visibility.Collapsed;
                            spFullyQualifiedClassName.Visibility = Visibility.Collapsed;
                            spRoutineName.Visibility = Visibility.Collapsed;

                        }
                        break;
                    case "number validation":
                        {
                            spValidationSize.Visibility = Visibility.Collapsed;
                            spValidationValues.Visibility = Visibility.Collapsed;
                            spValidationDateFormat.Visibility = Visibility.Collapsed;
                            spDLLName.Visibility = Visibility.Collapsed;
                            spFullyQualifiedClassName.Visibility = Visibility.Collapsed;
                            spRoutineName.Visibility = Visibility.Collapsed;

                        }
                        break;
                    case "date validation":
                        {
                            spValidationSize.Visibility = Visibility.Collapsed;
                            spValidationValues.Visibility = Visibility.Collapsed;
                            spValidationDateFormat.Visibility = Visibility.Visible;
                            spDLLName.Visibility = Visibility.Collapsed;
                            spFullyQualifiedClassName.Visibility = Visibility.Collapsed;
                            spRoutineName.Visibility = Visibility.Collapsed;
                        }
                        break;
                    case "external routine call":
                        {
                            spValidationSize.Visibility = Visibility.Collapsed;
                            spValidationValues.Visibility = Visibility.Collapsed;
                            spValidationDateFormat.Visibility = Visibility.Collapsed;
                            spDLLName.Visibility = Visibility.Visible;
                            spFullyQualifiedClassName.Visibility = Visibility.Visible;
                            spRoutineName.Visibility = Visibility.Visible;
                        }
                        break;
                }
            }
        }


        private void btnLoadValidationRuleXML_Click(object sender, RoutedEventArgs e)
        {
            DataView dtValidationRules = dgValidationRule.ItemsSource as DataView;
            DataTable dt = dtValidationRules.Table;

            dt.Rows.Clear();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(filePath))
                {

                    //logger.Info("Inside the ValidationRuleFile.GetInstance() method.");
                    //ValidationRuleFile validations = new ValidationRuleFile();
                    //validations.ValidationRules = new List<ValidationsRule>();
                    try
                    {
                        //logger.Info("Loading the Validation Xml File.");
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(filePath);

                        XmlNodeList validationNodes = xmlDoc.SelectNodes("//ValidationRules/ValidationRule");

                        foreach (XmlNode validationNode in validationNodes)
                        {
                            DataRow dr = dt.NewRow();
                            //ValidationsRule rule = new ValidationsRule();
                            if (validationNode.Attributes["ValidationId"] != null)
                            {

                                dr["ValidationId"] = validationNode.Attributes["ValidationId"].Value;

                            }




                            if (validationNode.Attributes["ValidationName"] != null)
                            {

                                dr["ValidationName"] = validationNode.Attributes["ValidationName"].Value;

                            }



                            if (validationNode.Attributes["ColumnNumber"] != null)
                            {

                                dr["ColumnNumber"] = validationNode.Attributes["ColumnNumber"].Value;

                            }



                            //logger.Info("Fetching Validation Type element.");
                            XmlNode validationTypeNode = validationNode.SelectSingleNode("ValidationType");
                            if (validationTypeNode != null)
                            {

                                dr["ValidationType"] = validationTypeNode.InnerText.Trim();

                            }





                            //logger.Info("Fetching Validation Size element.");
                            XmlNode validationSizeNode = validationNode.SelectSingleNode("ValidationSize");
                            if (validationSizeNode != null)
                            {

                                dr["ValidationSize"] = Convert.ToInt32(validationSizeNode.InnerText.Trim());

                            }



                            //logger.Info("Fetching Error Message element.");
                            XmlNode errorMessageNode = validationNode.SelectSingleNode("ErrorMessage");
                            if (errorMessageNode != null)
                            {

                                dr["ValidationErrorMessage"] = errorMessageNode.InnerText.Trim();
                                //logger.Info("Fetched Error Message element : " + rule.ErrorMessage);
                            }



                            //logger.Info("Fetching Values element.");
                            XmlNode valuesNode = validationNode.SelectSingleNode("Values");
                            if (valuesNode != null)
                            {
                                XmlNodeList valuesList = valuesNode.SelectNodes("value");
                                if (valuesList != null)
                                {
                                    List<string> Values = new List<string>();
                                    foreach (XmlNode valNode in valuesList)
                                    {
                                        Values.Add(valNode.InnerText.Trim());
                                    }
                                    string values = String.Join(" ", Values);
                                    dr["ValidationValues"] = values;
                                }

                                //logger.Info("Fetched Values element : " + rule.Values.Count);
                            }


                            //logger.Info("Fetching Date Format element.");
                            XmlNode dateFormatNode = validationNode.SelectSingleNode("DateFormat");
                            if (dateFormatNode != null)
                            {

                                dr["DateFormate"] = dateFormatNode.InnerText.Trim();
                                //logger.Info("Fetched Date Format element : " + rule.ErrorMessage);
                            }



                            //rule.DLLInfo = new DLLDetails();
                            XmlNode DLLNameNode = validationNode.SelectSingleNode("DLL/DLLName");
                            if (DLLNameNode != null)
                            {

                                dr["DLLName"] = DLLNameNode.InnerText.Trim();
                                //logger.Info("Fetched DLLName element : " + rule.DLLInfo.DLLName);
                            }


                            XmlNode fullyQualififedClassNameNode = validationNode.SelectSingleNode("DLL/FullyQualififedClassName");
                            if (fullyQualififedClassNameNode != null)
                            {

                                dr["FullyQualifiedClassName"] = fullyQualififedClassNameNode.InnerText.Trim();
                                //logger.Info("Fetched FullyQualififedClassName element : " + rule.DLLInfo.FullyQualififedClassName);
                            }


                            XmlNode routineNameNode = validationNode.SelectSingleNode("DLL/RoutineName");
                            if (routineNameNode != null)
                            {

                                dr["RoutineName"] = routineNameNode.InnerText.Trim();
                                /*logger.Info("Fetched Routine Name element : " + rule.DLLInfo.RoutineName);

                                if (routineNameNode.Attributes["IsStaticMethod"] != null)
                                {

                                    rule.DLLInfo.IsStaticMethod = Convert.ToBoolean(routineNameNode.Attributes["IsStaticMethod"].Value);
                                    logger.Info("Fetched IsStaticMethod " + rule.DLLInfo.IsStaticMethod);
                                }


                                if (routineNameNode.Attributes["ReturnType"] != null)
                                {

                                    rule.DLLInfo.ReturnType = routineNameNode.Attributes["ReturnType"].Value;
                                    logger.Info("Fetched ReturnType " + rule.DLLInfo.ReturnType);
                                }



                                if (routineNameNode.Attributes["InputType"] != null)
                                {

                                    rule.DLLInfo.InputType = routineNameNode.Attributes["InputType"].Value;
                                    logger.Info("Fetched InputType " + rule.DLLInfo.InputType);
                                }*/
                            }




                            dt.Rows.Add(dr);
                            //validations.ValidationRules.Add(rule);
                        }
                    }
                    catch (Exception excp)
                    {
                        MessageBox.Show("Error in parsing the Validation XML file : " + excp.ToString() + " --- " + excp.StackTrace);

                    }




                    dgValidationRule.ItemsSource = dt.DefaultView;
                    //return validations;
                }
            }
        }
        private void btnGenerateSettingsXML_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtTimeSetting.Text.Trim()))
            {
                MessageBox.Show("Please provide Time Setting.");
                return;
            }


            if (string.IsNullOrEmpty(txtPercentageSetting.Text.Trim()))
            {
                MessageBox.Show("Please provide Percentage Setting.");
                return;
            }


            StringBuilder buildSettingsXML = new StringBuilder();

            buildSettingsXML.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

            buildSettingsXML.AppendLine("<Settings>");
            buildSettingsXML.AppendLine("<RunSettings>");
            buildSettingsXML.AppendLine($"<NewRun>{chkIsNewRun.IsChecked.ToString()}</NewRun>");
            buildSettingsXML.AppendLine("</RunSettings>");
            buildSettingsXML.AppendLine("<ValidationSetting>");
            buildSettingsXML.AppendLine($"<Percentage>{txtPercentageSetting.Text.Trim()}</Percentage>");
            buildSettingsXML.AppendLine($"<Time>{txtTimeSetting.Text.Trim()}</Time>");
            buildSettingsXML.AppendLine("</ValidationSetting>");
            buildSettingsXML.AppendLine("</Settings>");


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, buildSettingsXML.ToString());
            }
        }

        private void btnLoadSettingsXML_Click(object sender, RoutedEventArgs e)
        {

            //logger.Info("Inside the SettingsFile.GetInstance() method.");
            //SettingsFile settings = new SettingsFile();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;

                //logger.Info("Loading the Flat File Xml.");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);



                try
                {
                    //logger.Info("Retrieving the LogLevel element.");
                    //XmlNode logLevelNode = xmlDoc.SelectSingleNode("//Settings/LoggerSettings/LogLevel");
                    //if (logLevelNode != null)
                    //{
                    //    logger.Info("LogLevel element is exist.");
                    //    settings.LogLevel = logLevelNode.InnerText.Trim().ToLower();
                    //}
                    //else
                    //{
                    //    logger.Info("LogLevel element is not exist.");
                    //    settings.LogLevel = string.Empty;
                    //}


                    //logger.Info("Retrieving the NewRun element.");
                    XmlNode newRunNode = xmlDoc.SelectSingleNode("//Settings/RunSettings/NewRun");
                    if (newRunNode != null)
                    {
                        chkIsNewRun.IsChecked = Convert.ToBoolean(newRunNode.InnerText.Trim());
                    }
                    


                    //logger.Info("Retrieving the Percentage element.");
                    XmlNode percentageNode = xmlDoc.SelectSingleNode("//Settings/ValidationSetting/Percentage");
                    if (percentageNode != null)
                    {
                        txtPercentageSetting.Text = percentageNode.InnerText.Trim();
                    }
                    


                    //logger.Info("Retrieving the Time element.");
                    XmlNode timeNode = xmlDoc.SelectSingleNode("//Settings/ValidationSetting/Time");
                    if (timeNode != null)
                    {
                        txtTimeSetting.Text = timeNode.InnerText.Trim();
                        //settings.Time = Convert.ToInt32(timeNode.InnerText.Trim().ToLower());
                    }
                    
                }
                catch (Exception excp)
                {
                    MessageBox.Show("Error in parsing the Settings XML : " + excp.ToString() + " --- " + excp.StackTrace);
                    //settings = null;
                }
                //return settings;
            }
        }
        //btnLoadSettingsXML_Click
    }
}
