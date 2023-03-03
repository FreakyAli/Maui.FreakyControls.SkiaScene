﻿namespace Samples;

public partial class AppShell : Shell
{
    internal const string skeleton = "Skeleton";
    //internal const string textInputLayout = "TextInputLayouts";
    //internal const string pickers = "Pickers";
    //internal const string imageViews = "ImageViews";
    //internal const string signatureView = "SignatureView";
    //internal const string signaturePreview = "ImageDisplay";
    //internal const string checkboxes = "Checkboxes";
    //internal const string radioButtons = "RadioButtons";

    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(skeleton, typeof(SkeletonEffect.SkeletonEffectView));
        //Routing.RegisterRoute(pickers, typeof(Pickers.PickersView));
        //Routing.RegisterRoute(textInputLayout, typeof(TextInputLayout.TextInputLayoutView));
        //Routing.RegisterRoute(imageViews, typeof(ImageViews.ImagesPage));
        //Routing.RegisterRoute(signatureView, typeof(SignatureView.SignatureView));
        //Routing.RegisterRoute(signaturePreview, typeof(SignatureView.ImageDisplay));
        //Routing.RegisterRoute(checkboxes, typeof(Checkboxes.CheckboxesView));
        //Routing.RegisterRoute(radioButtons, typeof(RadioButtons.RadioButtonsView));
    }
}

