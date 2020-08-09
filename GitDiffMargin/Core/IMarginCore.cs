﻿using System;
using System.Windows;
using System.Windows.Media;
using PerforceDiffMargin.Git;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace PerforceDiffMargin.Core
{
    internal interface IMarginCore
    {
        event EventHandler BrushesChanged;

        event EventHandler<HunksChangedEventArgs> HunksChanged;

        IWpfTextView TextView { get; }
        string OriginalPath { get; }
        FontFamily FontFamily { get; }
        FontStretch FontStretch { get; }
        FontStyle FontStyle { get; }
        FontWeight FontWeight { get; }
        double FontSize { get; }
        Brush Background { get; }
        Brush Foreground { get; }
        Brush AdditionBrush { get; }
        Brush ModificationBrush { get; }
        Brush RemovedBrush { get; }
        double EditorChangeLeft { get; }
        double EditorChangeWidth { get; }
        double ScrollChangeWidth { get; }
        double ScaleFactor { get; }
        void MoveToChange(int lineNumber);
        bool RollBack(HunkRangeInfo hunkRangeInfo);
        ITextDocument GetTextDocument();
        void FocusTextView();
    }
}