   M S B A M L   `   `   `  ����  ManagerLibrary0    *ManagerLibrary.Views.client.RegisterBorrow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Register Borrow Book��5   	   $	��371��6&   $	��406��63    � WindowStartupLocation$ CenterScreen=�5   	   ���	#FF2C3E50.��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Đăng kí mượn sách��6!   $��28(�6:   $
��Bold"�6H   
�������$��Center=�6m   $��	0,20,0,20q�6�   �� 5   
   $	��Top=�6   $��Center=�6-   $	��374��6J   ���	#FF2C3E50$	��331��6m   $��0,77,0,0q�6z   $��2��6�   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��
10,10,10,0q�62   .+�5      �� $��Tên sách: ��6   
�������$	��120��6A   $��Center=�6M   �� 5      -   5         ��Name$ txtTitle��5      $	��200��6.   $��Center=�6:   
�������6U   5      �� 5      $ 
Horizontal=�6   $��
10,10,10,0q�62   .+�5      �� $��Ngày hẹn trả: ��6   
�������$	��120��6E   $��Center=�6Q   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ 	dpDueDate��5      $	��200��60   $��Center=�6<   5      �� 5      $ 
Horizontal=�6   $��
10,10,10,0q�62   .+�5      �� $��
Ghi chú: ��6   
�������$	��120��6@   $	��Top=�6L   $��0,5,0,0q�6d   �� 5      -   5         ��Name$ tbNotes��5      $	��200��6+   $	��Top=�67    �� AcceptsReturn$
 True��6O   $	��100��6d    �� TextWrapping$
 Wrap=�6q   5      �� 5!      $ 
Horizontal=�6   $��Center=�62   $��
20,20,20,0q�6O   .+�5"      �� -   5        $��Xác nhận��5"      $	��100��6,   $��30��68   ���	#FF2980B9
�������$
��Bold"�6�   $��10q�6�   �� 5#      -   5        $��Hủy��5#      $	��100��6'   $��30��63   ���	#FF2980B9
�������$
��Bold"�6~   $��10q�6�   5$      5%      5&      5'      