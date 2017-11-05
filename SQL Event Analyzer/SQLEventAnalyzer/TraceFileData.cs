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

using System;

public class TraceFileData
{
	public int Id;
	public string Type;
	public string TextData;
	public int Spid;
	public long Duration;
	public DateTime StartTime;
	public long Reads;
	public long Writes;
	public long Cpu;
	public long Rows;

	public TraceFileData(int id, string type, string textData, int spid, long duration, DateTime startTime, long reads, long writes, long cpu, long rows)
	{
		Id = id;
		Type = type;
		TextData = textData;
		Spid = spid;
		Duration = duration;
		StartTime = startTime;
		Reads = reads;
		Writes = writes;
		Cpu = cpu;
		Rows = rows;
	}
}
