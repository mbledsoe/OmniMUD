﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniMud.Core.Commands
{
	public interface ICommand
	{
		void Execute(CommandContext context);
	}
}