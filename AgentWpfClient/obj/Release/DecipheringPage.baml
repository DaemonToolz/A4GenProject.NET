   M S B A M L   `   `   `  ���� I  EAgentWpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AgentWpfClientAgentWpfClient  #    AgentWpfClient.DecipheringPag   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:AgentWpfClient       N� Title$  DecipheringPag��$	��450��$	��520��.��� 
�������.+��� -    ��Name$ BackBtn��$
��Left=�$	��Top=�$��45��$��45��$��Center=�����  �� 
TargetType" ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��	BackImage��$��Images/BackBtn.png��$��35��$��35��$��Uniform=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ GridWrapper�� �� Property � �� �� Value �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q��� -   	 ��Name$	 DocumentTitle��$��Document��$
��Left=�$��
10,160,0,0q�$	��Top=�$	��100��$��37��$��Center=��� -   $ FileLoaderButton��$
��Left=�$��474,160,0,0q�$	��Top=�$��36��
�������
�������$��Center=�$��37������ " ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��	BackImage��$��Images/UploadFile.png��$��30��$��30��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q��� -   
 ��Name$
 FileContentViewer��$
��Left=�$	��118��$��	8,200,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��461�� �� 
IsReadOnly . �� -    ��Name$ DocumentTitleLabel��$��WrapWithOverflow=�$
��Left=�$��115,160,0,0q�$	��Top=�$	��354��$��37���� -   $ SendFileBtn��$
��Left=�$��474,202,0,0q�$	��Top=�$��34��$��34��
�������
�������$��Center=�{�.  ���� " ��.R���  ��Name$ GridWrapperBorder��
�������._��  �Name$ GridWrapper��
�������.+���  ��Name$ 	BackImage��$��Images/SendFileIcon.png��$��25��$��25��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q��� -   $	 DecryptTitle��$��Décryptage��$
��Left=�$��
10,323,0,0q�$	��Top=�$��90���� -   $	 DecryptStatus��$��
Non lancé��$
��Left=�$��115,323,0,0q�$	��Top=�$	��393���� -	   $	 ContactMailLabel��$��Votre email��$
��Left=�$��	10,45,0,0q�$	��Top=�$	��100���� -
   $
 EmailTextBox��$
��Left=�$��26��$��
115,45,0,0q�$
 Wrap=�$	��Top=�$	��273��
�������$��Center=��� -   $	 WarningMessage��$y�Hidden=�$��Email invalide��$
��Left=�$��
393,45,0,0q�$	��Top=�$	��115��
���  ���� -   $
 KeyBeginTextBox��$
��Left=�$��25��$��
115,71,0,0q�$
 Wrap=�$	��Top=�$	��150��
�������$��Center=��� -   $	 KeyBeginlabel��$��Début clé (Opt)��$
��Left=�$��	10,71,0,0q�$	��Top=�$	��100���� -   $
 KeySizeTextBox��$
��Left=�$��26��$��
395,71,0,0q�$
 Wrap=�$	��Top=�$	��115��
�������$��Center=��� -   $	 KeySizeLabel��$��Taille clé (Opt)��$
��Left=�$��
278,70,0,0q�$	��Top=�$	��110���� -    ��Name$ CipheringTypeCombobox��
�������$
��Left=�$��115,129,0,0q�$	��Top=�$	��393��$��26��
�����������  �� Path$ CipheringTypes�����  �� 
TargetType" ��.���  � ��
 �������  � ��
 ������� -   $	 CipheringTypeLabel��$��Chiffrement��$
��Left=�$��
10,129,0,0q�$	��Top=�$	��100��#   AgentWpfClient.LoadingSpinner  -     Name$ WaitingSpinner��$
��Left=�$��85��$��198,220,0,0q�$	��Top=�$��85���� -   $	 KeyEndLabel��$��Fin de clé (Opt)��$
��Left=�$��	10,98,0,0q�$	��Top=�$	��100���� -   $
 KeyEndTextBox��$
��Left=�$��25��$��
115,96,0,0q�$
 Wrap=�$	��Top=�$	��150��
�������$��Center=��� -   $	 DecipheringPercentage��$
��Left=�$��115,349,0,0q�$	��Top=�$	��150��$��25���� -   $ 	 DecipheringPercentageLabel��$��Pourcentage��$
��Left=�$��
10,349,0,0q�$	��Top=�$��90���� -   $	 DecipheringRoundsLabel��$��Clés tentées��$
��Left=�$��
10,374,0,0q�$	��Top=�$��90���� -   $	 DecipheringRounds��$
��Left=�$��115,374,0,0q�$	��Top=�$	��150��$��26���� -   $	 DecipheringETALabel��$��Opérations estimées��$
��Left=�$��278,374,0,0q�$	��Top=�$	��122���� -   $	 DecipheringETA��$
��Left=�$��405,374,0,0q�$	��Top=�$	��103��$��26���� -   $ GenerateTextFile��{�.  $
��Left=�$��474,236,0,0q�$	��Top=�$��34��
�������
�������$��Center=�$��34������ " ��.R��� $ GridWrapperBorder��
�������._�� $ GridWrapper��
�������.+��� $ 	BackImage��$��Images/Download.png��$��25��$��25��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q�