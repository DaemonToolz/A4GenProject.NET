   M S B A M L   `   `   `  ���� I  EAgentWpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AgentWpfClientAgentWpfClient       AgentWpfClient.OptionsPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:AgentWpfClient     $	��450��$	��520��  N� Title$  OptionsPage��.��� 
�������.+��� -    ��Name$ BackBtn��$
��Left=�$	��Top=�$��45��$��45��$��Center=�����  �� 
TargetType" ��.R���  ��Name$ GridWrapperBorder��
�������._��  �Name$ GridWrapper��
�������.+���  ��Name$ 	BackImage��$��Images/BackBtn.png��$��35��$��35��$��Uniform=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ��� 	 �� 
TargetName$	 GridWrapper��
 �� Property
 � �� �� Value �	#FFEBE7E7�� $	 GridWrapperBorder��
 � �� �	#FFE8E8E8�� $	 GridWrapperBorder��
 � ��$ 2q��� -    ��Name$ Options��$
��Left=�$	��385��$��0,65,0,-0.4q�$	��Top=�$	��520�� �� TabStripPlacement$
 Left=����	#FFDDDDDD$��2q�$��1,1,0,0q���Z� .@��� 
���������Z� ���� $��Images/SecurityOption.png��$��50��$��50��.��� .+��� -    ��Name$ SecurityDefaultCheckbox��$E��?Utiliser les paramètres relatifs à la sécurité, par défaut��$
��Left=�$��	10,10,0,0q�$	��Top=�$��Center=�$	��418��� -   $ SecuritySettings��$
��Left=�$��88��$��	10,44,0,0q�$	��Top=�$	��430��.+�G� -    G�Name$ DefaultPasswordPassBox��$
��Left=�$��	88,33,0,0q�$	��Top=�$	��332��$��Center=�$��26��$��1,0,0,0q�$��0q� � UseLayoutRounding . �� -    ��Name$ DefaultUsernameTextbox��$
��Left=�$��26��$��88,5,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��332��$��Center=�$��1,0,0,0q��� -    ��Name$ DefaultUserLabel��$��Username��$
��Left=�$��0,5,0,0q�$	��Top=�$��83���� -   $ DefaultPwdLabel��$��Password��$
��Left=�$��0,33,0,0q�$	��Top=�$��83���� -	   $ TokenOnStart��$7��1Générer un token au démarrage de l'application��$��	3,64,10,0q�$��Center=��� -
   $ AddEncryption��{�.  $��Ajouter Chiffrement��$
��Left=�$��
10,179,0,0q�$	��Top=�$	��128��$��23����Z� ��Z� �� -   $ EncryptionName��{�.  $
��Left=�$��23��$��143,179,0,0q�$
 Wrap=�$	��Top=�$	��298��$��Center=�$��1,0,0,0q��� 
���������Z� ���� $��Images/Information.png��$��50��$��50��.��� .+��� -   $ DefaultCheckbox��$6��0Utiliser les paramètres généraux, par défaut��$
��Left=�$��	10,10,0,0q�$	��Top=�$��Center=�$	��418��� -   $ GlobalSettings��$
��Left=�$	��248��$��	10,44,0,0q�$	��Top=�$	��430��.+��� -   $ DefaultPathLabel��$��Chemin par défaut��$
��Left=�$��	10,10,0,0q�$	��Top=�$��48��$��Center=��� -   $ DefaultPathTextBox��$
��Left=�$��48��$��
124,10,0,0q�$
 Wrap=�$	��Top=�$	��296��$��Center=� �� SelectionBrush �	#FFDAE1E7$��1,0,0,0q����	#FFB1B1B1
��������� -   $ DefaultEmailLabel��$��Email par défaut��$
��Left=�$��	10,63,0,0q�$	��Top=��� -   $ DefaultEmailTextBox��$
��Left=�$��26��$��
124,63,0,0q�$
 Wrap=�$	��Top=�$	��296��$��Center=� �	#FFDAE1E7$��1,0,0,0q����	#FFB1B1B1
��������� -   $ SaveChangesBtn��$
��Left=�$��
480,25,0,0q�$	��Top=�$��40��$��40����Z� ��Z� ���� " ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��	BackImage��$��Images/SaveSettings.png��$��35��$��35��$��Uniform=� P�  �@J�IsMouseOver . . ��� $	 GridWrapper��
 � �� �	#FFEBE7E7�� $	 GridWrapperBorder��
 � �� �	#FFE8E8E8�� $	 GridWrapperBorder��
 � ��$ 2q�