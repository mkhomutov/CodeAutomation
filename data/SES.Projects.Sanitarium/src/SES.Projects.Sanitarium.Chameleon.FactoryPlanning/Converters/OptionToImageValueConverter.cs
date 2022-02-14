namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using Catel.MVVM.Converters;
    using Gum.Fonts;
    using Orc.Theming;

    public class OptionToImageValueConverter : ValueConverterBase<string>
    {
        private readonly Dictionary<string, ImageSource> _items = new();

        public OptionToImageValueConverter()
        {
            _items = new Dictionary<string, ImageSource>
            {
                {QuantityTypes.Batches,  new FontImage(FontAwesome5.Coins){FontFamily = "FontAwesome5"}.GetImageSource()},
                {QuantityTypes.Cases,  new FontImage(FontAwesome5.PalletAlt){FontFamily = "FontAwesome5"}.GetImageSource()},
                {QuantityTypes.Duration,  new FontImage(FontAwesome5.Clock){FontFamily = "FontAwesome5"}.GetImageSource()},
            };
        }

        #region Methods
        protected override object Convert(string value, Type targetType, object parameter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return _items.TryGetValue(value, out var image) ? image : null;
        }
        #endregion
    }
}
