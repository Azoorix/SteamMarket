   M S B A M L   `   `   `  ����F  BSteamMarket, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SteamMarketSteamMarket  5   	       SteamMarket.SellWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SteamMarket     5   	     � Title$  Окно продажи��5   	   $	��380��6   $	��500��6+    � 
ResizeMode$ NoResize=�67   .��5	      � ���#101822.+�5
   
   �� $	��450��6   .+�5      �� $��0 0 0 20q�6      txt#��[  6*   $��25(�6G   $��Center=�6U   $%��Продажа предмета��6r   6�   �� 5       �� Orientation$ 
Horizontal=�6   $��0 0 0 40q�62   .+�5      �� $	��100��6   $	��100��6"   $
��Left=�6.   $��0 0 10 0q�6I   ���#ffffff$��1q�6q   ._�5      �� -   5        $	��itm��5   1   $	��100��6   $	��100��6$   6?   5      �� 5      $ Vertical=�6   .+�5      �� -   5        $��
TBlockName��5       #��[  62   6S   �� 5      -   5        $��
TBlockGame��5       #��[  62   ���#32353C6f   5      5      �� 5      $	��430��6   .+�5      �� $	��300��6   $��Center=�6)   .+�5      �� $
��Left=�6    $)��#Укажите количество��6;   #��[  6U   6u   �� 5      -   5         ��Name$ 
TBoxAmount��5   .    �� 	MaxLength$ 6��6   6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� $��	20 10 0 0q�6!   $
��Left=�64   $��Вы получите ��6O   $��15(�6c   #��[  6q   �� 6�   -   5         ��Name$ TBlockMoney��5   �   6�   6�   5      �� 5       -   5        $��errorMessage��5    c   $X��RМаксимальная сумма для разовой продажи 150000₽��6   
���  ��6y   �� 5!      -   5        $��BtnSell��5!   /   $��Продать��6>   $��Right=�6P     btn#��[ 6l   ��5"      �� 5#       � SpreadMethod$ Reflect=�6.    �� 
StartPoint$	 0,0&�6E    �� EndPoint$	 0,1&�6V   .��5$      � 	 � Color$	 #46602a��6+   
 � Offset$
 1Q�6;   6H   � 5%      $	 #82B93C��6+   $
 0Q�6;   6H   5&      5(      5*      5+      5,      5-      