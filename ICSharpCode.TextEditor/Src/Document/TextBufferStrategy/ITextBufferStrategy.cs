﻿/*
Copyright (C) 2005 SharpDevelop

Modified 2017 by Lars Hove Christiansen
http://virtcore.com

This file is a part of ICSharpCode.TextEditor

	This library is free software; you can redistribute it and/or modify it
	under the terms of the GNU Lesser General Public License as published
	by the Free Software Foundation; either version 2.1 of the License, or
	(at your option) any later version.

	This library is distributed in the hope that it will be useful, but
	WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
	General Public License for more details.

	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

namespace ICSharpCode.TextEditor.Document
{
	/// <summary>
	/// Interface to describe a sequence of characters that can be edited. 	
	/// </summary>
	public interface ITextBufferStrategy
	{
		/// <value>
		/// The current length of the sequence of characters that can be edited.
		/// </value>
		int Length
		{
			get;
		}

		/// <summary>
		/// Inserts a string of characters into the sequence.
		/// </summary>
		/// <param name="offset">
		/// offset where to insert the string.
		/// </param>
		/// <param name="text">
		/// text to be inserted.
		/// </param>
		void Insert(int offset, string text);

		/// <summary>
		/// Removes some portion of the sequence.
		/// </summary>
		/// <param name="offset">
		/// offset of the remove.
		/// </param>
		/// <param name="length">
		/// number of characters to remove.
		/// </param>
		void Remove(int offset, int length);

		/// <summary>
		/// Replace some portion of the sequence.
		/// </summary>
		/// <param name="offset">
		/// offset.
		/// </param>
		/// <param name="length">
		/// number of characters to replace.
		/// </param>
		/// <param name="text">
		/// text to be replaced with.
		/// </param>
		void Replace(int offset, int length, string text);

		/// <summary>
		/// Fetches a string of characters contained in the sequence.
		/// </summary>
		/// <param name="offset">
		/// Offset into the sequence to fetch
		/// </param>
		/// <param name="length">
		/// number of characters to copy.
		/// </param>
		string GetText(int offset, int length);

		/// <summary>
		/// Returns a specific char of the sequence.
		/// </summary>
		/// <param name="offset">
		/// Offset of the char to get.
		/// </param>
		char GetCharAt(int offset);

		/// <summary>
		/// This method sets the stored content.
		/// </summary>
		/// <param name="text">
		/// The string that represents the character sequence.
		/// </param>
		void SetContent(string text);
	}
}
