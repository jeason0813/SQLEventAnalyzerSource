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

public class Column
{
	public string Name;
	public IsolationLevelType IsolationLevel;
	public string Input;
	public ColumnType InputType;
	public string Output;
	public ColumnType OutputType;
	public bool Hidden;
	public bool Enabled;
	public int Width;

	public Column(string name, IsolationLevelType isolationLevel, string input, ColumnType inputType, string output, ColumnType outputType, bool hidden, bool enabled, int width)
	{
		Name = name;
		IsolationLevel = isolationLevel;
		Input = input;
		InputType = inputType;
		Output = output;
		OutputType = outputType;
		Hidden = hidden;
		Enabled = enabled;
		Width = width;
	}

	public override string ToString()
	{
		return Name;
	}

	public enum ColumnType
	{
		SQL,
		Constant,
		RegEx,
		StoredProcedureName,
		StoredProcedureParameter,
		LogParameter
	}

	public enum IsolationLevelType
	{
		ReadUncommitted,
		ReadCommitted,
		RepeatableRead,
		Serializable
	}
}
