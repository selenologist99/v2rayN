using System.Reactive.Disposables;
using System.Windows;
using ReactiveUI;

namespace v2rayN.Views;

public partial class DNSSettingWindow
{
    private static Config _config;

    public DNSSettingWindow()
    {
        InitializeComponent();

        this.Owner = Application.Current.MainWindow;
        _config = AppHandler.Instance.Config;

        ViewModel = new DNSSettingViewModel(UpdateViewHandler);

        cmbdomainStrategy4Freedom.ItemsSource = Global.DomainStrategy4Freedoms;
        cmbdomainStrategy4Out.ItemsSource = Global.SingboxDomainStrategy4Out;
        cmbdomainDNSAddress.ItemsSource = Global.DomainDNSAddress;
        cmbdomainDNSAddress2.ItemsSource = Global.SingboxDomainDNSAddress;

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.UseSystemHosts, v => v.togUseSystemHosts.IsChecked).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.DomainStrategy4Freedom, v => v.cmbdomainStrategy4Freedom.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.DomainDNSAddress, v => v.cmbdomainDNSAddress.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.NormalDNS, v => v.txtnormalDNS.Text).DisposeWith(disposables);

            this.Bind(ViewModel, vm => vm.DomainStrategy4Freedom2, v => v.cmbdomainStrategy4Out.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.DomainDNSAddress2, v => v.cmbdomainDNSAddress2.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.NormalDNS2, v => v.txtnormalDNS2.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.TunDNS2, v => v.txttunDNS2.Text).DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.SaveCmd, v => v.btnSave).DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.ImportDefConfig4V2rayCmd, v => v.btnImportDefConfig4V2ray).DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.ImportDefConfig4SingboxCmd, v => v.btnImportDefConfig4Singbox).DisposeWith(disposables);
        });
        WindowsUtils.SetDarkBorder(this, AppHandler.Instance.Config.UiItem.CurrentTheme);
    }

    private async Task<bool> UpdateViewHandler(EViewAction action, object? obj)
    {
        switch (action)
        {
            case EViewAction.CloseWindow:
                this.DialogResult = true;
                break;
        }
        return await Task.FromResult(true);
    }

    private void linkDnsObjectDoc_Click(object sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://xtls.github.io/config/dns.html#dnsobject");
    }

    private void linkDnsSingboxObjectDoc_Click(object sender, RoutedEventArgs e)
    {
        ProcUtils.ProcessStart("https://sing-box.sagernet.org/zh/configuration/dns/");
    }
}
