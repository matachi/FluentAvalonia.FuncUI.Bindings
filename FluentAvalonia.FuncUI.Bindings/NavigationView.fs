namespace FluentAvalonia.FuncUI.Bindings

open System.Collections
open Avalonia.FuncUI.Builder
open Avalonia.FuncUI.DSL
open Avalonia.FuncUI.Types
open FluentAvalonia.UI.Controls

[<AutoOpen>]
module NavigationView =

    let create (attrs: IAttr<NavigationView> list) : IView<NavigationView> =
        ViewBuilder.Create<NavigationView>(attrs)

    type NavigationView with

        static member menuItemsSource<'t when 't :> NavigationView>(value: IEnumerable) : IAttr<NavigationView> =
            AttrBuilder<NavigationView>
                .CreateProperty<IEnumerable>(NavigationView.MenuItemsSourceProperty, value, ValueNone)

        static member selectedItem<'t when 't :> NavigationView>(item: obj) : IAttr<'t> =
            AttrBuilder<'t>
                .CreateProperty<obj>(NavigationView.SelectedItemProperty, item, ValueNone)

        static member onSelectedItemChanged<'t when 't :> NavigationView>(func: obj -> unit, ?subPatchOptions) =
            AttrBuilder<'t>
                .CreateSubscription<obj>(NavigationView.SelectedItemProperty, func, ?subPatchOptions = subPatchOptions)
