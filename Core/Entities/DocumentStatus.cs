using System;
using System.ComponentModel;

namespace Core.Entities
{
    public enum DocumentStatus
    {
        [Description("Any user, including unregistered users, can search for and download the document.")]
        Public,

        [Description("Only registered users can search for and download the document.")]
        Private,

        [Description("Only Admin and those who added the document can search for and download it.")]
        Confidential
    }
}