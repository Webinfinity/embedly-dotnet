namespace Embedly.OEmbed
{
    /// <summary>
    /// oEmbed request options
    /// </summary>
    public class RequestOptions
    {
		public enum EmbedWmode
		{
			Window = 0,
			Opaque = 1,
			Transparent = 2
		}

        /// <summary>
        /// Gets or sets the maximum width of the content.
        /// </summary>
        /// <value>
        /// The maximum width.
        /// </value>
        public int MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum height of the content.
        /// </summary>
        /// <value>
        /// The maximum height.
        /// </value>
        public int MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to strip styling.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [no style]; otherwise, <c>false</c>.
        /// </value>
        public bool NoStyle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to auto play videos.
        /// </summary>
        /// <value>
        ///   <c>true</c> if auto play; otherwise, <c>false</c>.
        /// </value>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets the number of words to limit the response to.
        /// </summary>
        /// <value>
        /// The words.
        /// </value>
        public int Words { get; set; }

        /// <summary>
        /// Gets or sets the number of chars to limit the response to.
        /// </summary>
        /// <value>
        /// The chars.
        /// </value>
        public int Chars { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to force embedly to reevaluate the link 
        /// </summary>
        /// <value>
        ///   <c>true</c> if force reevaluation; otherwise, <c>false</c>.
        /// </value>
        public bool Force { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use SSL to embed
		/// </summary>
		/// <value>
		///   <c>true</c> if SSL is to be used; otherwise, <c>false</c>.
		/// </value>
		public bool Secure { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether embeds are created in an iframe
		/// </summary>
		/// <value>
		///   <c>true</c> if embeds should be in an iframe; otherwise, <c>false</c>.
		/// </value>
		public bool Frame { get; set; }

		/// <summary>
		/// Gets or sets a value indicating the embed wmode value
		/// </summary>
		/// <value>
		///   <c>EmbedWmode</c> enum.
		/// </value>
		public EmbedWmode? Wmode { get; set; }

        /// <summary>
        /// Builds query-string parameters based on the request options set
        /// </summary>
        /// <returns></returns>
        internal string GetQueryString()
        {
            string querystring = string.Empty;

            if (MaxWidth > 0)
                querystring += "&maxwidth=" + MaxWidth;

            if (MaxHeight > 0)
                querystring += "&maxheight=" + MaxHeight;

            if (Width > 0)
                querystring += "&width=" + Width;

            if (NoStyle)
                querystring += "&nostyle=true";

            if (AutoPlay)
                querystring += "&autoplay=true";

            if (Words > 0)
                querystring += "&words=" + Words;

            if (Chars > 0)
                querystring += "&chars=" + Chars;

            if (Force)
                querystring += "&force=true";

			if (Secure)
				querystring += "&secure=true";

			if (Frame)
				querystring += "&frame=true";

			if (Wmode.HasValue)
			{
				querystring += "&wmode=";

				switch (Wmode.Value)
				{
					case EmbedWmode.Window:
						querystring += "window";
						break;
					case EmbedWmode.Opaque:
						querystring += "opaque";
						break;
					case EmbedWmode.Transparent:
						querystring += "transparent";
						break;
				}
			}

            return querystring;
        }
    }
}