   M S B A M L   `   `   `  ���� K  GN3PS.GenerateXml, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:N3PS.GenerateXmlN3PS.GenerateXml  !    N3PS.GenerateXml.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:N3PS.GenerateXml       � Title$  Generate XML��$	��600��$	��960��.���� .@��� $��	Flat File��.����  �� Orientation$ 
Horizontal=�.+��� 
�������
�������$��1q�$	��850�� �� CornerRadius$ 8,8,3,3��._��� $��5,15,5,5q�$	��800��.+��� -   $��
btnLoadXML��$��Load XML��$��5,5,5,5q�$��30���� 
�������
�������$��1q�$ 8,8,3,3��._�� $��	15,10,5,5q� � ColumnDefinitions �� $	��200 ��� $	��600 � � RowDefinitions �� $��30 ��� $��30 �.+��� -    ��Name$ chkHeaderExist��$��0��$��0��chkHeaderExist��.��Header Exist�� $��1��$��0��$ 
Horizontal=�.+���  �� Target ��  �� ElementName$ txtRecordSize��.��Record Size�� -    ��Name$ txtRecordSize��$��5,5,5,5q�$��70��$��20��$	��Top=��� $��0��$��1��$��2��$ 
Horizontal=�.+���  �� $ txtFlatFilePath��.��Flat File Path�� -   $ txtFlatFilePath��$��5,5,5,5q�$	��600��$��20��$	��Top=��� -   $��btnOpenFileDialog��$�� ... ��$��5,5,5,5q��� 
�������
�������$��1q�$ 8,8,3,3��$��0,15,0,0q�._��  �� $	��100 ��� $	��700 � �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��70 �.+��� $��0��$��0��$��2��$ 
Horizontal=�.+���  �� $ txtFieldName��.��
Field Name�� -   $ txtFieldName��$��5,5,5,5q�$	��670��$��20���� $��0��$��1��$��2��$ 
Horizontal=�.+���  �� $ txtColumnNumber��.��Column Number�� -   $ txtColumnNumber��$��5,5,5,5q�$	��643��$��20���� $��0��$��2��$��2��$ 
Horizontal=�.+���  �� $ txtSequenceNumber��.��Sequence Number�� -   $ txtSequenceNumber��$��5,5,5,5q�$	��633��$��20���� $��0��$��3��$��2��$ 
Horizontal=�.+���  �� $ 	txtLength��.��Length�� -	   $ 	txtLength��$��5,5,5,5q�$	��693��$��20���� -
   	 ��Name$	 btnAddField��$��	Add Field��$��4��$��0��$��2��$��15,15,45,15q�
 �� 	ClickMode$
 Press=�&   System.Windows.Controls.DataGrid  -     Name$ dgFields��   AutoGenerateColumns .  $��15,15,15,15q�$	��660��   CanUserAddRows .     CanUserDeleteRows .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 
Field Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	FieldName+  $ Column Number��$	 150  �� *ColumnNumber+  $ Sequence Number��$	 100  �� *SquenceNumber+  $ Length��$	 100  �� *Length+4  .System.Windows.Controls.DataGridTemplateColumn  $ Delete��$ 80    CellTemplate �� .R��� $��btnGridFieldsDelete��-   .��Delete�� -   $	 btnGenerateXML��$��Generate XML��$��15,15,25,15q�$��50�� � LayoutTransform ��  �� Angle$	 -90Q��� $��Validation Rule��.���  �� $��* � �� $	��900 ��� $��60 �.+��� $
��Auto=�$��0��$��0��.���� 
�������
�������$��1q�$	��880��$ 8,8,3,3��._��� .+��� 
�������
�������$��1q�$ 8,8,3,3��$��0,15,0,0q�$��40��$	��Top=�._��� -   $��btnLoadValidationRuleXML��$��Validation Rule��$��5,5,5,5q��� 
�������
�������$��1q�$ 8,8,3,3��$��5,15,0,0q�$	��Top=�._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $	��800 �.+��� $ 
Horizontal=�$��0��$��0��$��5,5,5,5q�.+��� $��Validation Id���� -   $��txtValidationId��$	��500��$��30��$	��Top=��� $ 
Horizontal=�$��1��$��0��$��5,5,5,5q�.+��� $��Validation Name���� -   $��txtValidationName��$	��500��$��30��$	��Top=��� $ 
Horizontal=�$��2��$��0��$��5,5,5,5q�.+��� $��Column Number���� -   $��txtValidationColumnNumber��$	��500��$��30��$	��Top=��� $ 
Horizontal=�$��3��$��0��$��5,5,5,5q�.+��� $��Validation Type���� -   $��cbValidationType��$	��300��$��30��.@���  �� 
IsSelected . .��Length Validation�� .��Mandatory Check�� .��Value Validation�� .��Number Validation�� .��Date Validation�� .��External Routine Call�� -   $��spValidationSize��$ 
Horizontal=�$��4��$��0��$��5,5,5,5q�.+��� $��Validation Size���� -   $��txtValidationSize��$	��500��$��30��$	��Top=��� $ 
Horizontal=�$��5��$��0��$��5,2,5,5q�.+��� $��Validation Error Message���� -   $��txtValidationErrorMessage��$	��500��$��30��$	��Top=��� -   $��spValidationValues��$ 
Horizontal=�$��6��$��0��$��5,2,2,5q�.+��� $��Validation Values���� -   $��txtValidationValues��$	��500��$��30��$	��Top=��� -   $��spValidationDateFormat��$ 
Horizontal=�$��7��$��0��$��5,2,2,5q�.+��� $��Date Format���� -   $��txtDateFormat��$	��500��$��30��$	��Top=��� -   $��	spDLLName��$ 
Horizontal=�$��8��$��0��$��5,2,2,5q�.+��� $��DLL Name���� -   $��
txtDLLName��$	��500��$��30��$	��Top=��� -   $��spFullyQualifiedClassName��$ 
Horizontal=�$��9��$��0��$��5,2,2,5q�.+��� $ ��Fully Qualified Class Name���� -   $ ��txtFullyQualifiedClassName��$	��500��$��30��$	��Top=��� -   $��spRoutineName��$ 
Horizontal=�$��10��$��0��$��5,2,2,5q�.+��� $��Routine Name���� -   $��txtRoutineName��$	��500��$��30��$	��Top=��� -    $��btnAddValidationRule��$��Add Validation Rule��$��11��$��0��$	��600��$��30��$	��5,5q��� $
��Auto=�.��  -!   $ dgValidationRule�� .  $��15,15,15,15q�$
��1400�� .   .    
IsReadOnly .    $ Validation Id��$ 80  �� *ValidationId+  $ Validation Name��$	 100  �� *ValidationName+  $ Column Number��$	 100  �� *ColumnNumber+  $ Validation Type��$	 100  �� *ValidationType+  $ Validation Size��$	 100  �� *ValidationSize+  $ Validation Error Message��$	 150  �� *ValidationErrorMessage+  $ Validation Values��$	 120  �� *ValidationValues+  $ Date Formate��$	 100  �� *DateFormate+  $ DLL Name��$	 200  �� *	DLLName+  $  Fully Qualified Class Name��$	 200  �� *FullyQualifiedClassName+  $ Routine Name��$	 100  �� *RoutineName+  $ Delete��$ 80  �� .R��� $"��btnGridValidationRulesDelete��-"   .��Delete�� -#   $	 btnGenerateValidationXML��$��Generate XML��$��5,5,20,5q�$��40��$��1��$��0�� �� $	 -90Q��� $��Settings��.���� 
�������
�������$��1q�$ 8,8,3,3��$��5,15,0,0q�$	��Top=�._�� $��5,5,5,5q� �� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � �� $	��900 �.+��� -$   $��btnLoadSettingsXML��$��Load XML��$��0��$��5,5,5,5q�$��30���� $��1��$��0��$ 
Horizontal=�.+��� -%   $��chkIsNewRun��$��
Is New Run���� $��2��$��0��$ 
Horizontal=�$��5,5,5,5q�.+��� $��Time Setting���� -&   $��txtTimeSetting��$	��400��$��30���� $��3��$��0��$ 
Horizontal=�$��5,5,5,5q�.+��� $��Percentage Setting���� -'   $��txtPercentageSetting��$	��400��$��30���� -(   $��btnGenerateSettingsXML��$��Generate XML��$��4��$��5,5,5,5q�$��30��