in your viewmodel(exampleViewModel.cs):write the following 
 Messenger.Default.Send<string>("focus", "DoFocus");

now in your View.cs(not the XAML the view.xaml.cs) write the following in the constructor
 public MyView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "DoFocus", doFocus);
        }
        public void doFocus(string msg)
        {
            if (msg == "focus")
                this.txtcode.Focus();
        }

that method owrks just fine and with less code and maintaining MVVM standards
