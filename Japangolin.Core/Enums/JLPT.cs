namespace Wacton.Japangolin.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using Wacton.Japangolin.Core.Properties;

public static class JLPT
{
    /* 
     * JLPT data taken from Jonathan Waller's JLPT resources page (as referenced by Jisho.org)
     * http://www.tanos.co.uk/jlpt/jlpt5/vocab/
     * -----
     * data was parsed with my \Tools\JLPT-Parser\
     * unmatched sequences were added by hand
     */

    public static readonly List<int> N5 = ReadSequenceData(Resources.JLPT_N5);

    private static List<int> ReadSequenceData(string file)
    {
        var lines = file.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        var splitLines = lines.Select(line => line.Split(','));
        var sequences = splitLines.Where(values => !string.IsNullOrEmpty(values[0])).Select(values => int.Parse(values[0]));
        return sequences.Distinct().ToList();
    }
        
}