
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #[$Namespace(name)]#;

internal static class ScopeNames
{
    //public const string CsvClassName = "CsvClassName";
    #[$ConstScopeNames(csvList.className)]#
}

internal static class FileNames
{
    //public const string CsvFileName = "CsvFileName.csv";
	%FILENAMES%
}
