﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using DDay.Collections;

namespace DDay.iCal
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class CalendarParameterList :
        GroupedValueList<string, ICalendarParameter, CalendarParameter, string>,
        ICalendarParameterCollection
    {
        #region Private Fields

        ICalendarObject m_Parent;
        bool m_CaseInsensitive;

        #endregion

        #region Constructors

        public CalendarParameterList()
        {
        }

        public CalendarParameterList(ICalendarObject parent, bool caseInsensitive)
        {
            m_Parent = parent;
            m_CaseInsensitive = caseInsensitive;

            ItemAdded += new EventHandler<ObjectEventArgs<ICalendarParameter,int>>(OnParameterAdded);
            ItemRemoved += new EventHandler<ObjectEventArgs<ICalendarParameter,int>>(OnParameterRemoved);
        }

        #endregion

        #region Protected Methods

        protected void OnParameterRemoved(object sender, ObjectEventArgs<ICalendarParameter, int> e)
        {
            e.First.Parent = null;
        }

        protected void OnParameterAdded(object sender, ObjectEventArgs<ICalendarParameter, int> e)
        {
            e.First.Parent = m_Parent;
        }

        #endregion

        #region Overrides
        
        protected override string GroupModifier(string group)
        {
            if (m_CaseInsensitive)
                return group.ToUpper();
            return group;
        }

        #endregion
    }
}