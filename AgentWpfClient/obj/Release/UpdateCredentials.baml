   M S B A M L   `   `   `  ���� I  EAgentWpfClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AgentWpfClientAgentWpfClient  &     AgentWpfClient.UpdateCredentials   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:AgentWpfClient       N� Title$  UpdateCredentials��$	��450��$	��520��.��� 
�������.+��� -    ��Name$ BackBtn��$
��Left=�$	��Top=�$��45��$��Center=�$��41������  �� 
TargetType" ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��	BackImage��$��Images/BackBtn.png��$��35��$��35��$��Uniform=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ GridWrapper�� �� Property � �� �� Value �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q�G� -   	 G�Name$	 PasswordBox��$
��Left=�$��
120,77,0,0q�$	��Top=�$	��280��$��Center=�$��1,0,0,0q�$��26���� -   
 ��Name$
 UserName��$
��Left=�$��23��$��
120,52,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��280��$��Center=�$��1,0,0,0q����� *!UserCredentials.DefaultUserName+�� -   $ ValidateBtn��$
��Left=�$��
415,53,0,0q�$	��Top=�$��75��$��75�� J� RenderTransformOrigin$ 0.5,0.5&���Z� ���� " ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��SkipBack��$#��Images/ValidateConnection.png��$��50��$��50��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q��� -    ��Name$ ResultsTitle��$��Résulat��$
��Left=�$��
10,105,0,0q�$	��Top=�$��70���� -   $ ResultsLabel��$�� ��$
��Left=�$��115,105,0,0q�$	��Top=�$	��285��$��Center=��� -   $
 ServeurResponse��$
��Left=�$	��149��$��
10,136,0,0q�$
 Wrap=�$	��Top=�$	��498�� �� 
IsReadOnly . �� -   $ UsernameLabel��$��Utilisateur��$
��Left=�$��	10,52,0,0q�$	��Top=�$��24��$��Center=�$	��105���� -	   $ PasswordLabel��$��Mot de passe��$
��Left=�$��	10,77,0,0q�$	��Top=�$��28��$��Center=�$	��105��#   AgentWpfClient.LoadingSpinner  -
     Name$ WaitingSpinner��$y�Hidden=�$
��Left=�$	��150��$��190,290,0,0q�$	��Top=�$	��150���� -   $ SkipBtn��$y�Hidden=�$
��Left=�$��216,297,0,0q�$	��Top=�$��75��$��75��$ 0.5,0.5&���Z� ��Z� ���� " ��.R��� $��GridWrapperBorder��
�������._�� $��GridWrapper��
�������.+��� $��SkipBack��$��Images/Encrypted.png��$��50��$��50��$��Uniform=� P�  �@J�IsMouseOver . . ��� $ GridWrapper�� � �� �	#FFEBE7E7�� $ GridWrapperBorder�� � �� �	#FFE8E8E8�� $ GridWrapperBorder�� � ��$ 2q�