   M S B A M L   `   `   `  ���� I  EAgentWpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AgentWpfClientAgentWpfClient      AgentWpfClient.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:AgentWpfClient       � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � Title$ ENIGMA��$	��450��$	��525��.��� 
�������.+�&    AgentWpfClient.CategorySelection  -     Name$ 
ChangeCred��&    AgentWpfClient.CategorySelection   	MouseDown$ CatSelect_MouseDown��{�.     ImageSource$ Images/identity.png��   CategoryName$- 'Générer une autorisation de connexion��$��0,155,0.8,234.6q�  -   $ SendCiphered��$! CategorySelection_MouseDown��{�.  $ Images/Encrypted.png��$% Déchiffrer un fichier en ligne��$��0,220,0.8,169.6q�  -   $ Options��$ Options_MouseDown��$ Images/Options.png��$ Options��$��0,285,0.8,104.6q��� -    ��Name$ Instruction��$�� ��$
��Left=�$��
10,385,0,0q�$	��Top=�$	��498���� -    ��Name$ MainIcon��$��Images/WhiteHatIcon.png��$
��Left=�$	��100��$��	45,35,0,0q�$	��Top=�$	��100���� -   $ 	MainTitle��$��ENIGMA��$
��Left=�$��
166,65,0,0q�$	��Top=�$��Georgia)�$��36(� � -   	  �Name$	 NavigationFrame��
  � NavigationUIVisibility$
 Hidden=�$
��Left=�$	��420��$��0,30,-0.2,-0.4q�$	��Top=�$	��525���� -    ��Name$ MenuBar�����	#FFF4F4F5$
��Left=�$��30��$	��Top=�$	��465���� -	    ��Name$ ExitBtn��$�� ��$��Center=�$��Center=�$
��Left=�$��495,0,-0.2,0q�$	��Top=�$��30��$��0q�$��30��$��32(�����  �� 
TargetType" ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��SkipBack��$��Images/Disconnect.png��$��20��$��20��$��Uniform=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ GridWrapper�� �� Property � �� �� Value �	#7FFF0000�� -
   $ MinimizeButton��$�� ��$��Center=�$��Center=�$
��Left=�$��	465,0,0,0q�$	��Top=�$��30��$��0q�$��30��$��32(����� " ��.R���  ��Name$ GridWrapperBorder��
�������._��  �Name$ GridWrapper��
�������.+��� $ SkipBack��$��Images/HideWindow.png��$��20��$��20��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#7FCBCBCB