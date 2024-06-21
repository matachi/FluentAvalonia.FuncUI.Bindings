namespace FluentAvalonia.FuncUI.Bindings.Sample

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.Hosts
open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open FluentAvalonia.FuncUI.Bindings
open FluentAvalonia.Styling
open FluentAvalonia.UI.Controls

module Main =
    let navigationViewItems =
        [ NavigationViewItem(Content = "Test 1")
          NavigationViewItem(Content = "Test 2")
          NavigationViewItem(Content = "Test 3") ]

    let saveSelectedNavigationViewItem (selectedNavigationViewItem: IWritable<NavigationViewItem>) (newlySelectedNavigationViewItem: obj) =
        match newlySelectedNavigationViewItem with
        | :? NavigationViewItem as newItem -> selectedNavigationViewItem.Set newItem
        | _ -> ()

    let view =
        Component(fun ctx ->
            let selectedNavigationViewItem = ctx.useState navigationViewItems[0]

            NavigationView.create
                [ NavigationView.menuItemsSource navigationViewItems
                  NavigationView.selectedItem selectedNavigationViewItem.Current
                  NavigationView.onSelectedItemChanged (saveSelectedNavigationViewItem selectedNavigationViewItem)
                  NavigationView.content (TextBlock.create [ TextBlock.text (selectedNavigationViewItem.Current.Content :?> string) ]) ])

type MainWindow() =
    inherit HostWindow()

    do
        base.Title <- "Sample app"
        base.Icon <- WindowIcon(System.IO.Path.Combine("Assets", "Logo64.png"))
        base.Content <- Main.view

type App() =
    inherit Application()

    override this.Initialize() = this.Styles.Add(FluentAvaloniaTheme())

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime -> desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

        base.OnFrameworkInitializationCompleted()
