﻿namespace Maui.FreakyEffects.TouchPress;

public interface ICustomAnimation
{
    Task SetAnimation(View view);

    Task RestoreAnimation(View view);
}