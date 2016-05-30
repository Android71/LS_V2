//http://www.codeproject.com/Tips/612994/Binding-with-Properties-defined-in-Code-Behind
//
//<DataGridTemplateColumn Width="{Binding Source={local:Root}, Path=ColumnOneWidth}"
// CellTemplate="{Binding Source={ local:Root }, Path=ColumnOneTemplate}"/>

using System;
using System.Windows.Markup;
using System.Xaml;

namespace LS_Designer_WPF.WPF_Extras
{
    public class RootExtention : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IRootObjectProvider provider = serviceProvider.GetService
            (typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (provider != null)
            {
                return provider.RootObject;
            }

            return null;
        }
    }
}
