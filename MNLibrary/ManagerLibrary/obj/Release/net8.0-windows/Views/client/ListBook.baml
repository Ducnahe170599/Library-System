   M S B A M L   `   `   `  ����   ManagerLibrary*    $ManagerLibrary.Views.client.ListBook   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Product List��$	��800��$
��1200�����	#FF2C3E50 � WindowState$ 	Maximized=� � 
ResizeMode$ NoResize=�.��� $��20q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+�� $��0��$��Center=����	#FF34495E$��60��$��0,0,0,10q� � ColumnDefinitions �� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��Center=�.+��� $X��RC:\Users\nguye\OneDrive\Desktop\MNLibrary\ManagerLibrary\Images\logo_MNLibrary.png��$��60��$��60��$��0,0,0,6q��� $��
ChongNovel��$��Center=�
�������$��24(�$��VnTimes)�$
��Bold"��� -   $��1��
������ 
������ $��10q�.���� $N��HC:\Users\nguye\OneDrive\Desktop\MNLibrary\ManagerLibrary\Images\home.png��$��44��$��40�� J� RenderTransformOrigin$ 	0.545,0.5&��� -    ��Name$ lvBook��$��1��$��0,10,0,0q����	#FF34495E$��1q����� .R�A�  A� Columns$ 5��	 A� Rows$	 2������ .R��� ���	#FF34495E
 �� CornerRadius$
 5��$��10q�._��� $	��160��$	��260��$��10q�{��� *	IsCheck+.+��� $	��100��$��0,0,0,10q����� *	ImagePath+ � ToolTip �� *Title+�� $
��Bold"�
�������$
��Wrap=�$��Center=�$��0,0,0,5q����� *Title+�� $ 
Horizontal=�$��Center=�$��0,0,0,5q�.+��� $��Tác giả: ��$
��Bold"�
��������� 
����������� *
AuthorName+�� $ 
Horizontal=�$��Center=�$��0,0,0,5q�.+��� $��Thể loại: ��$
��Bold"�
��������� 
����������� *CategoryName+�� $ 
Horizontal=�$��Center=�$��0,0,0,5q�.+��� $��Số lượng: ��$
��Bold"�
��������� 
����������� *
Quantity+�� $ 
Horizontal=�$��Center=�$��0,0,0,5q�.+��� $��Trạng thái: ��$
��Bold"�
��������� 
����������� *DisplayStatus+�� -   $��Đăng kí mượn sách��$��0,10,0,0q�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ Visible=� �� Triggers ��  �� Binding �� *
Quantity+ �� Value 0.a���  � y�$ 	Collapsed=��� $��2��$ 
Horizontal=�$��Center=�$��10q�.+��� -    ��Name$ btnPrevious��$��Previous��$	��100���� -   $ btnNext��$
��Next��$	��100��