/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of SQL Event Analyzer

	SQL Event Analyzer is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SQL Event Analyzer is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SQL Event Analyzer. If not, see <http://www.gnu.org/licenses/>.
*/

using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;

public class UserDefinedFunctions
{
	[SqlFunction(IsDeterministic = true)]
	public static string GetRegEx(string input, string regExPattern)
	{
		Regex regEx = new Regex(regExPattern);
		Match match = regEx.Match(input);

		if (match.Success)
		{
			return match.Groups[1].Value;
		}

		return "";
	}

	[SqlFunction(IsDeterministic = true)]
	public static bool MatchRegEx(string input, string regExPattern)
	{
		if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(regExPattern))
		{
			return false;
		}

		Regex regEx = new Regex(regExPattern);
		Match match = regEx.Match(input);
		return match.Success;
	}

	[SqlFunction(IsDeterministic = true)]
	public static string GetStoredProcedureName(string input, int occurrence)
	{
		int execEndPosition = -1;
		int startPosition = 0;

		for (int i = 1; i <= occurrence; i++)
		{
			execEndPosition = GetExecEndPosition(input, startPosition);
			startPosition = execEndPosition;
		}

		if (execEndPosition == -1)
		{
			return "";
		}

		NameObject nameObject = GetName(execEndPosition, input);
		return nameObject.Name;
	}

	[SqlFunction(IsDeterministic = true)]
	public static bool MatchStoredProcedureName(string input, string name, int occurrence)
	{
		int execEndPosition = -1;
		int startPosition = 0;

		for (int i = 1; i <= occurrence; i++)
		{
			execEndPosition = GetExecEndPosition(input, startPosition);
			startPosition = execEndPosition;
		}

		if (execEndPosition == -1)
		{
			return false;
		}

		NameObject nameObject = GetName(execEndPosition, input);

		if (nameObject.Name == name)
		{
			return true;
		}

		return false;
	}

	[SqlFunction(IsDeterministic = true)]
	public static string GetStoredProcedureParameter(string input, int position, int occurrence)
	{
		NameObject nameObject = GetStoredProcedureNameObject(input, occurrence);

		if (nameObject != null)
		{
			string parameters = input.Substring(nameObject.LastCharacterPositionInName).Trim();

			List<string> parameterArray = GetParameters(parameters);

			if (position > 0)
			{
				for (int i = 0; i < parameterArray.Count; i++)
				{
					if (i == position - 1)
					{
						return parameterArray[i];
					}
				}
			}
			else
			{
				return parameterArray.Count.ToString();
			}
		}

		return "";
	}

	[SqlFunction(IsDeterministic = true)]
	public static bool MatchStoredProcedureParameter(string input, int position, string value, int occurrence)
	{
		NameObject nameObject = GetStoredProcedureNameObject(input, occurrence);

		if (nameObject != null)
		{
			string parameters = input.Substring(nameObject.LastCharacterPositionInName).Trim();

			List<string> parameterArray = GetParameters(parameters);

			if (position > 0)
			{
				for (int i = 0; i < parameterArray.Count; i++)
				{
					if (i == position - 1)
					{
						if (parameterArray[i] == value)
						{
							return true;
						}
					}
				}
			}
			else
			{
				if (parameterArray.Count.ToString() == value)
				{
					return true;
				}
			}
		}

		return false;
	}

	[SqlFunction(IsDeterministic = true)]
	public static string GetLogParameter(string input, int position)
	{
		const int logEndPosition = 7; // -- Log:
		string parameters = input.Substring(logEndPosition).Trim();

		List<string> parameterArray = GetParameters(parameters);

		if (position > 0)
		{
			for (int i = 0; i < parameterArray.Count; i++)
			{
				if (i == position - 1)
				{
					return parameterArray[i];
				}
			}
		}
		else
		{
			return parameterArray.Count.ToString();
		}

		return "";
	}

	[SqlFunction(IsDeterministic = true)]
	public static bool MatchLogParameter(string input, int position, string value)
	{
		const int logEndPosition = 7; // -- Log:
		string parameters = input.Substring(logEndPosition).Trim();

		List<string> parameterArray = GetParameters(parameters);

		if (position > 0)
		{
			for (int i = 0; i < parameterArray.Count; i++)
			{
				if (i == position - 1)
				{
					if (parameterArray[i] == value)
					{
						return true;
					}
				}
			}
		}
		else
		{
			if (parameterArray.Count.ToString() == value)
			{
				return true;
			}
		}

		return false;
	}

	private static NameObject GetStoredProcedureNameObject(string input, int occurrence)
	{
		int execEndPosition = -1;
		int startPosition = 0;

		for (int i = 1; i <= occurrence; i++)
		{
			execEndPosition = GetExecEndPosition(input, startPosition);
			startPosition = execEndPosition;
		}

		if (execEndPosition == -1)
		{
			return null;
		}

		NameObject nameObject = GetName(execEndPosition, input);
		return nameObject;
	}

	private static List<string> GetParameters(string parameters)
	{
		List<string> parameterList = new List<string>();

		parameters = string.Format(",{0},", parameters);

		int beginString = -1;
		int endString = -1;
		bool previousCharAQuote = false;
		bool previousValueAString = false;
		int beginInt = -1;
		int endInt = -1;

		for (int i = 0; i < parameters.Length; i++)
		{
			if (beginString == -1 && parameters[i] == '\'')
			{
				beginString = i;
				beginInt = -1;
				endInt = -1;
			}
			else if (beginString != -1 && parameters[i] == '\'')
			{
				if (previousCharAQuote)
				{
					previousCharAQuote = false;
					continue;
				}

				if ((i != parameters.Length - 1 && parameters[i + 1] != '\'') || i == parameters.Length - 1)
				{
					endString = i;
					beginInt = -1;
					endInt = -1;
				}
				else
				{
					previousCharAQuote = true;
				}
			}
			else if (beginString == -1 && endString == -1 && beginInt == -1 && i > 0)
			{
				beginInt = i;

				if (previousValueAString)
				{
					beginInt++;
				}
			}
			else if (beginString == -1 && endString == -1 && beginInt != -1 && parameters[i] == ',')
			{
				endInt = i;
			}

			if (beginString != -1 && endString != -1)
			{
				string stringValue = parameters.Substring(beginString + 1, endString - (beginString + 1));
				parameterList.Add(stringValue);
				beginString = -1;
				endString = -1;

				previousValueAString = true;
			}
			else if (beginInt != -1 && endInt != -1)
			{
				string intValue = parameters.Substring(beginInt, endInt - beginInt);
				intValue = GetValueFromIntegerParameter(intValue);
				parameterList.Add(intValue);
				beginInt = -1;
				endInt = -1;

				previousValueAString = false;
			}
		}

		return parameterList;
	}

	private static string GetValueFromIntegerParameter(string intValue)
	{
		int equalPos = intValue.LastIndexOf('=');

		string returnValue = intValue;

		if (equalPos > 0)
		{
			returnValue = intValue.Substring(equalPos + 1, intValue.Length - (equalPos + 1));
		}

		return returnValue.Trim();
	}

	private static NameObject GetName(int execEndPosition, string input)
	{
		int startPosition = execEndPosition;

		string searchString = input.Substring(execEndPosition);

		for (int i = 0; i < searchString.Length; i++)
		{
			if (searchString[i] != ' ' && searchString[i] != '\r' && searchString[i] != '\n' && searchString[i] != '\t')
			{
				startPosition = i;
				break;
			}
		}

		int startBracketPosition = searchString.IndexOf("[", startPosition);
		int endSpacePosition = searchString.IndexOf(" ", startPosition);
		int endReturnPosition = searchString.IndexOf("\r", startPosition);
		int endNewLinePosition = searchString.IndexOf("\n", startPosition);
		int endTabPosition = searchString.IndexOf("\t", startPosition);

		bool bracketsInName = false;

		if (startBracketPosition >= 0
			&& ((startBracketPosition < endSpacePosition && endSpacePosition >= 0) || endSpacePosition == -1)
			&& ((startBracketPosition < endReturnPosition && endReturnPosition >= 0) || endReturnPosition == -1)
			&& ((startBracketPosition < endNewLinePosition && endNewLinePosition >= 0) || endNewLinePosition == -1)
			&& ((startBracketPosition < endTabPosition && endTabPosition >= 0) || endTabPosition == -1)
		)
		{
			bracketsInName = true;
		}

		int endPosition;

		if (bracketsInName)
		{
			int endBracketPosition = -1;
			int beginSearchForEndBracket = startBracketPosition;

			while (endBracketPosition == -1)
			{
				int oneEndBracketPosition = searchString.IndexOf("]", beginSearchForEndBracket);
				int twoEndBracketsposition = searchString.IndexOf("]]", beginSearchForEndBracket);

				if (oneEndBracketPosition == -1 && twoEndBracketsposition == -1)
				{
					return null;
				}

				if (oneEndBracketPosition != twoEndBracketsposition)
				{
					endBracketPosition = oneEndBracketPosition;
					break;
				}

				beginSearchForEndBracket = twoEndBracketsposition + 2;
			}

			endPosition = endBracketPosition + 1;
		}
		else
		{
			if (endSpacePosition != -1 && (endReturnPosition == -1 || (endReturnPosition != -1 && endSpacePosition <= endReturnPosition)) && (endNewLinePosition == -1 || (endNewLinePosition != -1 && endSpacePosition <= endNewLinePosition)) && (endTabPosition == -1 || (endTabPosition != -1 && endSpacePosition <= endTabPosition)))
			{
				endPosition = endSpacePosition;
			}
			else if (endReturnPosition != -1 && (endSpacePosition == -1 || (endSpacePosition != -1 && endReturnPosition <= endSpacePosition)) && (endNewLinePosition == -1 || (endNewLinePosition != -1 && endReturnPosition <= endNewLinePosition)) && (endTabPosition == -1 || (endTabPosition != -1 && endReturnPosition <= endTabPosition)))
			{
				endPosition = endReturnPosition;
			}
			else if (endNewLinePosition != -1 && (endReturnPosition == -1 || (endReturnPosition != -1 && endNewLinePosition <= endReturnPosition)) && (endSpacePosition == -1 || (endSpacePosition != -1 && endNewLinePosition <= endSpacePosition)) && (endTabPosition == -1 || (endTabPosition != -1 && endNewLinePosition <= endTabPosition)))
			{
				endPosition = endNewLinePosition;
			}
			else if (endTabPosition != -1 && (endReturnPosition == -1 || (endReturnPosition != -1 && endTabPosition <= endReturnPosition)) && (endNewLinePosition == -1 || (endNewLinePosition != -1 && endTabPosition <= endNewLinePosition)) && (endSpacePosition == -1 || (endSpacePosition != -1 && endTabPosition <= endSpacePosition)))
			{
				endPosition = endTabPosition;
			}
			else
			{
				endPosition = searchString.Length;
			}
		}

		string name = searchString.Substring(startPosition, endPosition - startPosition);
		int lastCharacterPositionInName = endPosition + execEndPosition;

		if (bracketsInName)
		{
			lastCharacterPositionInName += 1;
		}

		return new NameObject(name, lastCharacterPositionInName);
	}

	private static int GetExecEndPosition(string input, int startPosition)
	{
		string execCommand = "exec ";
		int execStartPosition = SearchForExecPosition(input, execCommand, startPosition);

		if (execStartPosition == -1)
		{
			execCommand = "execute ";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "exec\r";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "exec\n";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "exec\t";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "execute\r";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "execute\n";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition == -1)
		{
			execCommand = "execute\t";
			execStartPosition = SearchForExecPosition(input, execCommand, startPosition);
		}

		if (execStartPosition != -1)
		{
			return execStartPosition + execCommand.Length;
		}

		return -1;
	}

	private static int SearchForExecPosition(string input, string execCommand, int startPosition)
	{
		int execStartPosition = input.ToLower().IndexOf(string.Format("{0}", execCommand), startPosition);

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format("\n{0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format(" {0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format(";{0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format("){0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format("]{0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format("\r{0}", execCommand), startPosition);
		}

		if (execStartPosition == -1)
		{
			execStartPosition = input.ToLower().IndexOf(string.Format("\t{0}", execCommand), startPosition);
		}

		if (execStartPosition != -1)
		{
			int execEndPosition = execStartPosition + execCommand.Length;

			string searchString = input.Substring(execEndPosition);

			for (int i = 0; i < searchString.Length; i++)
			{
				if (searchString[i] == '(')
				{
					execStartPosition = SearchForExecPosition(input, execCommand, execEndPosition);

					if (execStartPosition != -1)
					{
						break;
					}
				}
				else if (searchString[i] != ' ' && searchString[i] != '\r' && searchString[i] != '\n' && searchString[i] != '\t')
				{
					break;
				}
			}
		}

		return execStartPosition;
	}

	private class NameObject
	{
		public readonly string Name;
		public readonly int LastCharacterPositionInName;

		public NameObject(string name, int lastCharacterPositionInName)
		{
			Name = name;
			LastCharacterPositionInName = lastCharacterPositionInName;
		}
	}
}
