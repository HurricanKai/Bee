// Copyright 2017 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;

namespace SerilogKonsole.Sinks.SystemConsole.Themes
{
    static class DefaultKonsoleThemes
    {
        public static DefaultKonsoleTheme Literate { get; } = new DefaultKonsoleTheme(
            new Dictionary<KonsoleThemeStyle, DefaultKonsoleThemeStyle>
            {
                [KonsoleThemeStyle.Text] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.SecondaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.TertiaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.Invalid] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Yellow },
                [KonsoleThemeStyle.Null] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Blue },
                [KonsoleThemeStyle.Name] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.String] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Cyan },
                [KonsoleThemeStyle.Number] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Magenta },
                [KonsoleThemeStyle.Boolean] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Blue },
                [KonsoleThemeStyle.Scalar] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Green },
                [KonsoleThemeStyle.LevelVerbose] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.LevelDebug] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.LevelInformation] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.LevelWarning] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Yellow },
                [KonsoleThemeStyle.LevelError] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
                [KonsoleThemeStyle.LevelFatal] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
            });

        public static DefaultKonsoleTheme Grayscale { get; } = new DefaultKonsoleTheme(
            new Dictionary<KonsoleThemeStyle, DefaultKonsoleThemeStyle>
            {
                [KonsoleThemeStyle.Text] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.SecondaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.TertiaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.Invalid] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.Null] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Name] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.String] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Number] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Boolean] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Scalar] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.LevelVerbose] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.LevelDebug] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.LevelInformation] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.LevelWarning] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.LevelError] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Black, Background = ConsoleColor.White },
                [KonsoleThemeStyle.LevelFatal] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Black, Background = ConsoleColor.White },
            });

        public static DefaultKonsoleTheme Colored { get; } = new DefaultKonsoleTheme(
            new Dictionary<KonsoleThemeStyle, DefaultKonsoleThemeStyle>
            {
                [KonsoleThemeStyle.Text] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray },
                [KonsoleThemeStyle.SecondaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.TertiaryText] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.Invalid] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Yellow },
                [KonsoleThemeStyle.Null] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Name] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.String] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Number] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Boolean] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.Scalar] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White },
                [KonsoleThemeStyle.LevelVerbose] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.Gray, Background = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.LevelDebug] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.DarkGray },
                [KonsoleThemeStyle.LevelInformation] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Blue },
                [KonsoleThemeStyle.LevelWarning] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.DarkGray, Background = ConsoleColor.Yellow },
                [KonsoleThemeStyle.LevelError] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
                [KonsoleThemeStyle.LevelFatal] = new DefaultKonsoleThemeStyle { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
            });
    }
}
