   M S B A M L   `   `   `  ����F  BSteamMarket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SteamMarket.PagesSteamMarket.Pages  5      !    SteamMarket.Pages.Inventory   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:SteamMarket.Pages     5        N� Title$  Инвентарь��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��95 �6   �� 5      $��* �6   .+�5   
   � -   5         �Name$ UserGrid��5      ���#101822$��Right=�67   $
��10 9q�6S   $	��Top=�6a   $	��275��6z   $��75��6�   .+�5      �� -   5        $��TBlockLogin��5   5      txt#��[  6   $��15(�6H    �� Padding$ 6 0 0 0q�6V   6j   �� 5      $��0 15 0 0q�6    �� Orientation$ 
Horizontal=�6+   .+�5      �� -   5         ��Name$ userimg��5      $��50��6)   $��50��64   $
��Left=�6@   $��6q�6[   �� 5      $ Vertical=�6   $��Center=�64   .+�5      �� $ 
Horizontal=�6!   .+�5      �� -   5         ��Name$ 
WalletLink��5   %   #��[  69   $��
0, 0, 0, 0q�6V   $��17(�6j   $��Center=�6x   �� 6�   ���#ffffff$��WalletPage.xaml?�6�   .�6�   Баланс:6�   6�   �� 5      -   5        $��TBlockBalance��5   $   #��[  69   $��16(�6V   $��
2, 2, 0, 0q�6d   6{   5      �� 5      -   5        $ InventoryLink��5   !   #��[  68   $��16(�6U   $��Center=�6c   �� 6�   ���#32353C$��InventoryPage.xaml?�6�   .�6�   Инвентарь6�   6�   5      5      5       �� 5#   
   -   5        $��LViewInventory��5#   I   $��1��6   $��0q�6    $��0 10 0 0q�65   $��Disabled=�6_   ��5$      �� 5%      6   ��5'      �� 5(      .R�5)      � $��20 0 0 0q�6     � Orientation$ 
Horizontal=�62   $��Center=�6K   6j   5*      ��5,      �� 5-      .R�5.      � $	��210��6   .+�50      �� $ Vertical=�6%   $	��240��6=   .+�51      �� $	��160��6)   .+�52   "   �� $	��100��6)   $��1q�66   ���#ffffff._�53   &   �� ��6,   �� *IdItemNavigation.Picture+6Z   54   #   �� 55   "   #��[  6,   $
��Wrap=�6I   $��15(�6]   $ 10q�6k   $
��Left=�6�   ��6x   �� *IdItemNavigation.Name+6�   56      �� 57      $ 
Horizontal=�6)   .+�58   "   �� #��[  6-   $��Количество: ��6K   6a   �� 59   "   $��Center=�6,   $
��auto��6I   #��[  6V   ��6s   �� *
AmountItem+6�   5:      �� 5;      .+�5<   "   �� $��BtnSell��6�   -   5          btn#��[ 5<   )   $��Продать��6F   $��0 30 0 0q�6X   ��5=   &   �� 5>   *    � SpreadMethod$ Reflect=�6>   	 �� 
StartPoint$		 0,0&�6U   
 �� EndPoint$	
 0,1&�6f   .��5?   .   �  � Color$ #46602a��6;    � Offset$ 1Q�6K   6X   � 5@   .   $ #82B93C��6;   $ 0Q�6K   6X   5A   +   5C   #   5D      5E      5F      5G      5I      5J      5K      