   M S B A M L   `   `   `  ����L  HAutDataProcSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AutDataProcSystemAutDataProcSystem  5      '    !AutDataProcSystem.MainGeneralPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AutDataProcSystem     5        N� Title$  MainGeneralPage��5	      .��5      � .+�5   
   �� $��/Resources/field1.jpeg��6   $��UniformToFill=�60   �� 5   
   
�������$��1q�6$   
������� J� Opacity$	 0.5Q�6K   �� 5   
    �� Orientation$ 
Horizontal=�6   $��62��6.   $	��163��6:   $
��Left=�6F   $	��Top=�6a   .+�5      �� $��/Resources/General.jpg��6   $��45��64   $
��Left=�6?   �� 5      $��Начальник��6   $	��100��6)   $��
10 24 10 0q�65   $��Arial)�6I   $��15(�6\   5      �� 5   
   $��Center=�6   $��0 20 0 0q�62   .+�5      �� -   5         ��Name$ organizationTextBlock��5      $��Book Antiqua)�67   $��28(�6Q   $��DemiBold"�6_   5      �� 5   
   $��50 150 50 100q�6   .+�5      �� -   5         ��Name$ comboBoxOrganization��5       �� Text$- 'Выберите организацию��65   $��14(�6Q   $��Book Antiqua)�6_    �� 
IsEditable . $	��200��6�   $��30��6�   $��0,0,0,10q�6�   &   System.Windows.Controls.DataGrid  5!      -   5          Name$ dataGridForms��5!      $	��120��6.      AutoGenerateColumns . ��. $
��Auto=�6{   $
��Auto=�6�   	   Columns	 5"      5%      5&      �� 5)   
   $��Bottom=�6   $��0 0 0 20q�60   .+�5*      �� -   5        $-��'Страница Авторизации��5*      $	��206��64   
 � 	Resources
 5+       �� 
TargetType �� Property �� Value��%^   '��       �� 5,      " ��6   .�5-      ��  �@��CornerRadius$ 5��69   5.      5/      ��50      �� 51       �� ResourceKey$ MainGeneral_Button��6%   53      54      �� 57   
   -   5        $��Показать��57      $
��Left=�6$   $��618,144,0,0q�6?   $	��Top=�6T   $��34��6l   $	��132��6x      MainGeneral_Button#��[  58   /    � 5:   
   -   5          �Name$ contentFrame��5:      $��2��6&   $��2��6:   5<      5=      