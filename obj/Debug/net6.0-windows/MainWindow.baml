   M S B A M L   `   `   `  ����F  BSteamMarket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SteamMarketSteamMarket  5   	       SteamMarket.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SteamMarket     5   	     � Title$  Steam Market��5   	   $	��650��6   $	��800��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��70* �6   �� 5      $
��180* �6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $	��69* �6   �� 5      $��70 �6   .+�5   
   � $��0��6   ���#101822$��2��61   .+�5      �� $��50 0 0 0q�6    �� Orientation$ 
Horizontal=�6+   $��Center=�6D   .+�5      �� -   5        $��	Market_PG��5   E   $��Center=�6   $��20q�68      
MainMarket#��[  6V   $��2��6z   $��Маркет��6�   6�   �� 5      -   5        $��Inventory_PG��5   F   $��Center=�6   $��20q�68   #��[  6Z   $��2��6~   $��Инвентарь��6�   �� 5      -   5        $��Operation_PG��5   F   $��Center=�6   $��20q�68   #��[  6Z   $��2��6~   $��Операции��6�   �� 5      -   5        $��
Profile_PG��5   F   $��Center=�6   $��20q�68   #��[  6X   $��2��6|   $��Профиль��6�   5      �� 5      -   5        $��LogOut��5   \   $��20q�6   $��Center=�6%   $��Right=�6@   #��[  6j   $��2��6�   $��
Выйти��6�   �� 5      -   5        $��Assets/logo_steam.png��5   H   $��2��6h   $	��150��6|   $
��30 0q�6�   $
��Left=�6�   6�   5!      � 5#   
   $��1��6   ���#1b2838$��2��61   .+�5$      �� $��Assets/highlight_bg.png��6   $��1��65   $��2��6C   $
��30 0q�6X   $��Center=�6f   $
��Fill=�6�   6�   5%      � 5&   
   $��2��6   ���#101822$��2��61    � 5(   
   -   5          �Name$ 	FrameMain��5(        � NavigationUIVisibility$ Hidden=�6#   $��1��6C   $��2��6P   6f   5)      5*      