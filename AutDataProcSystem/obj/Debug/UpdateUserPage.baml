   M S B A M L   `   `   `  ����L  HAutDataProcSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AutDataProcSystemAutDataProcSystem  5      &     AutDataProcSystem.UpdateUserPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AutDataProcSystem     5        N� Title$  UpdateUserPage��5	      .��5      � .+�5   
   �� $��/Resources/field1.jpeg��6   $��UniformToFill=�60   �� 5   
   
�������$��1q�6$   
������� J� Opacity$	 0.5Q�6K   �� 5   
    �� Orientation$ 
Horizontal=�6   $��62��6.   $	��180��6:   $
��Left=�6F   $	��Top=�6a   .+�5      �� $��/Resources/Admin.png��6   $��45��62   $
��Left=�6=   �� 5      $ ��Администратор��6   $	��120��6-   $��
10 24 10 0q�69   $��Arial)�6M   $��15(�6`   5      �� 5   
   $��Center=�6   $��0 20 0 0q�62   .+�5      �� $/��)Панель Администратора��6   $��Book Antiqua)�65   $��28(�6O   $��DemiBold"�6]   5      �� 5   
   $��Center=�6   $��	0 100 0 0q�62   .+�5      �� $;��5Редактирование пользователя��6   $��Book Antiqua)�6;   $��24(�6U   $��DemiBold"�6c   5      �� 5    
   -   5        $��Вернуться��5       $
��Left=�6%   $��
40,150,0,0q�6@   $	��Top=�6T   $��42��6l   $	��150��6x      BackToAuthPage_Button#��[  6�    � 	Resources 5!       �� 
TargetType �� Property �� Value��%^   '��       �� 5"      " ��6   .�5#      ��  �@��CornerRadius$ 5��65   5$      5%      5&      �� 5(   
   $��70 220 70 50q�6   .+�5*      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridUsers��5*      $	��120��6.      AutoGenerateColumns . ��. $
��Auto=�6{   $
��Auto=�6�   	   Columns	 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn
   Header$
 
Логин��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 95 68   6E     5-      $
 Пароль��6)   $ 95 69   6F     5.      $
 Имя��6)   $ 95 66   6C     5/      $
 Телефон��6)   $ 95 6:   6G     50      $
 
Почта��6)   $ 95 68   6E     51      $
 Организация��6)   $ 95 6>   6K     52      $
 Роль��6)   $ 95 67   6D   54      55      �� 57   
   -   5        $+��%Сохранить изменения��57      $��Center=�6/   $��
0,330,0,50q�6L   $��Bottom=�6`   $��42��6{   $	��150��6�   #��[  6�    58      ��%^   '��       �� 59      " ��6   .�5:      ��  �@��CornerRadius$ 5��65   5;      5<      5=       � 5@   
   -   5          �Name$ contentFrame��5@      $��1��6&   $��1��6:   5A      5B      