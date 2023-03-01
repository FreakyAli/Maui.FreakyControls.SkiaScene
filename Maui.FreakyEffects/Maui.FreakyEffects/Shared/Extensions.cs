﻿using System;
using Maui.FreakyEffects.TouchTracking;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Internals;
using Maui.FreakyEffects.Skeleton;
#if ANDROID
using PlatformTouchEffects = Maui.FreakyEffects.Platforms.Android.TouchEffect;
#elif IOS
using PlatformTouchEffects = Maui.FreakyEffects.Platforms.iOS.TouchEffect;
#endif


namespace Maui.FreakyEffects;

public static class Extensions
{
    public static void InitFreakyEffects(this IEffectsBuilder effects)
    {
        effects.Add<TouchEffect, PlatformTouchEffects>();
    }

    public static void InitSkiaSharp(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.UseSkiaSharp();
    }
}


internal static class ViewExtensions
{
    #region Internal Methods

    /// <summary>
    /// Check if property has dynamic color resource associated
    /// </summary>
    /// <typeparam name="TView">View type</typeparam>
    /// <param name="view">View object</param>
    /// <param name="property">Bindable property to check</param>
    /// <returns>True if any associated Setter found</returns>
    internal static bool HasDynamicColorOnProperty<TView>(this TView view, BindableProperty property) where TView : View
    {
        var resourceValue = view?.GetPropertyDynamicResourceValue(property);
        if (resourceValue == null) return false;

        var currentValue = GetValueFromPropertyName(view, property.PropertyName);
        return currentValue != null && (Color)currentValue == (Color)resourceValue;
    }

    /// <summary>
    /// Get value from dynamic resource associated to property
    /// </summary>
    /// <typeparam name="TView">View type</typeparam>
    /// <param name="view">View object</param>
    /// <param name="property">Bindable property to check</param>
    /// <returns>Associated resource value</returns>
    internal static object GetPropertyDynamicResourceValue<TView>(this TView view, BindableProperty property) where TView : View
    {
        var resourceKey = view.GetPropertyDynamicResourceKey(property);

        if (string.IsNullOrWhiteSpace(resourceKey))
            return null;

        var currentResources = Application.Current.Resources;
        if (!currentResources.TryGetValue(resourceKey, out var resourceValue))
            return null;

        return resourceValue;
    }

    /// <summary>
    /// Get key from dynamic resource associated to property
    /// </summary>
    /// <typeparam name="TView">View type</typeparam>
    /// <param name="view">View object</param>
    /// <param name="propertyToCheck">Bindable property to check</param>
    /// <returns>Associated resource key</returns>
    internal static string GetPropertyDynamicResourceKey<TView>(this TView view, BindableProperty propertyToCheck) where TView : View
    {
        var elementStyle = view?.Style;
        var styleSetters = GetAllSetters(elementStyle);
        var setter = styleSetters?.FirstOrDefault(
            s => s.Property.PropertyName == propertyToCheck.PropertyName);

        if (setter?.Value is DynamicResource dynamicResource)
            return dynamicResource.Key;

        return null;
    }

    #endregion

    #region Private Methods

    private static IEnumerable<Setter> GetAllSetters(Style style)
    {
        if (style == null)
            return Enumerable.Empty<Setter>();

        return style.Setters.Concat(GetAllSetters(style.BasedOn));
    }

    private static object GetValueFromPropertyName(object view, string propertyToCheck) =>
        view.GetType().GetProperty(propertyToCheck).GetValue(view);

    #endregion
}

[ContentProperty(nameof(Source))]
public sealed class DefaultAnimationExtension : IMarkupExtension<BaseAnimation>
{
    public int Interval { get; set; } = 500;

    public double? Parameter { get; set; }

    public AnimationTypes Source { get; set; }

    public BaseAnimation ProvideValue(IServiceProvider serviceProvider)
    {
        switch (Source)
        {
            case AnimationTypes.Beat:
                return new BeatAnimation(Interval, Parameter);
            case AnimationTypes.Fade:
                return new FadeAnimation(Interval, Parameter);
            case AnimationTypes.VerticalShake:
                return new VerticalShakeAnimation(Interval, Parameter);
            case AnimationTypes.HorizontalShake:
                return new HorizontalShakeAnimation(Interval, Parameter);
            case AnimationTypes.None:
            default:
                return null;
        }
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
        ProvideValue(serviceProvider);
}