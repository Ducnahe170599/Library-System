   M S B A M L   `   `   `  ����   ManagerLibrary0    *ManagerLibrary.Views.client.RegisterBorrow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Register Borrow Book��$	��371��$	��406�� � WindowStartupLocation$ CenterScreen=����	#FF2C3E50.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��Đăng kí mượn sách��$��28(�$
��Bold"�
�������$��Center=�$��	0,20,0,20q��� $	��Top=�$��Center=�$	��374�����	#FF2C3E50$	��331��$��0,77,0,0q�$��2��.+���  �� Orientation$ 
Horizontal=�$��
10,10,10,0q�.+��� $��Tên sách: ��
�������$	��120��$��Center=��� -    ��Name$ txtTitle��$	��200��$��Center=�
��������� $ 
Horizontal=�$��
10,10,10,0q�.+��� $��Ngày hẹn trả: ��
�������$	��120��$��Center=�(  "System.Windows.Controls.DatePicker  -     Name$ 	dpDueDate��$	��200��$��Center=��� $ 
Horizontal=�$��
10,10,10,0q�.+��� $��
Ghi chú: ��
�������$	��120��$	��Top=�$��0,5,0,0q��� -    ��Name$ tbNotes��$	��200��$	��Top=� �� AcceptsReturn$
 True��$	��100�� �� TextWrapping$
 Wrap=��� $ 
Horizontal=�$��Center=�$��
20,20,20,0q�.+��� -   $��Xác nhận��$	��100��$��30�����	#FF2980B9
�������$
��Bold"�$��10q��� -   $��Hủy��$	��100��$��30�����	#FF2980B9
�������$
��Bold"�$��10q�