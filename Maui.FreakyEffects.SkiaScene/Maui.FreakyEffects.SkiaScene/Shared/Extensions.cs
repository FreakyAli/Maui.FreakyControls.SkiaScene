﻿using System;
using Maui.FreakyEffects.TouchTracking;
using SkiaSharp.Views.Maui.Controls.Hosting;
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

