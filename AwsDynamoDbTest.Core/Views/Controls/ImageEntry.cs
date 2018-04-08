using System;
using Xamarin.Forms;

namespace AwsDynamoDbTest.Core.Views
{
    public class ImageEntry : Entry
    {
        public ImageEntry()
        {
            TextColor = CodeConstants.ImageEntry.Colour.TEXT;
            //PlaceholderColor = CodeConstants.ImageEntry.Colour.PLACEHOLDER; //This colour is defined in RegistrationPage.xaml
            ImageHeight = CodeConstants.ImageEntry.Dimensions.IMAGE_HEIGHT;
            ImageWidth = CodeConstants.ImageEntry.Dimensions.IMAGE_WIDTH;

            if (Device.RuntimePlatform == Device.Android)
                FontSize = CodeConstants.ImageEntry.Font.Size.ANDROID;
            else if (Device.RuntimePlatform == Device.iOS)
                FontSize = CodeConstants.ImageEntry.Font.Size.IOS;

            if (Device.RuntimePlatform == Device.Android)
                HeightRequest = CodeConstants.ImageEntry.Dimensions.HeightRequest.ANDROID;
            else if (Device.RuntimePlatform == Device.iOS)
                HeightRequest = CodeConstants.ImageEntry.Dimensions.HeightRequest.IOS;
        }
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageEntry), string.Empty);

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Xamarin.Forms.Color), typeof(ImageEntry), CodeConstants.ImageEntry.Colour.LINE);

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ImageEntry), 40);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ImageEntry), 40);

        public static readonly BindableProperty ImageAlignmentProperty =
            BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(ImageEntry), ImageAlignment.Left);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public ImageAlignment ImageAlignment
        {
            get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
            set { SetValue(ImageAlignmentProperty, value); }
        }
    }

    public enum ImageAlignment
    {
        Left,
        Right
    }
}
