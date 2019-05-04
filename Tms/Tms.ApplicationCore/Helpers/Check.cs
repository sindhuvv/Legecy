using System;
using System.Collections.Generic;
using System.Linq;
using Tms.ApplicationCore.Extensions;

namespace Tms.ApplicationCore.Helpers
{
	/// <summary>
	/// Argument checking class to make the if null throw exception easier to work with.
	/// </summary>
	public static class Check
	{
		/// <summary>
		/// Will check if the Object is null and throw an null argument exception.  The message parameter is optional.
		/// if( value == null ) throw Exception()
		/// </summary>
		/// <param name="value">The value to check against null.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default won't suffice.</param>
		public static void Null(Object value, String parameterName, String message = null)
		{
			if (value == null)
			{
				if (message == null)
					throw new ArgumentNullException(parameterName, Formatter.Null(parameterName));

				throw new ArgumentNullException(parameterName, message);
			}
		}

		/// <summary>
		/// Will check if the Object is not null and throw an argument exception.  The message parameter is optional.
		/// if( value != null ) throw Exception()
		/// </summary>
		/// <param name="value">The value to check against not null.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default won't suffice.</param>
		public static void NotNull(Object value, String parameterName, String message = null)
		{
			if (value != null)
			{
				if (message == null)
					throw new ArgumentException(parameterName, Formatter.NotNull(parameterName));

				throw new ArgumentException(parameterName, message);
			}
		}

		/// <summary>
		/// Will verify that the String is null or empty and throw an null argument exception.  The message parameter is optional.
		/// if( String.IsNullOrEmpty(value) ) throw Exception()
		/// </summary>
		/// <param name="value">The String to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default won't suffice.</param>
		public static void NullOrEmpty(String value, String parameterName, String message = null)
		{
			if (String.IsNullOrEmpty(value))
			{
				if (message == null)
					throw new ArgumentNullException(parameterName, Formatter.NullOrEmpty(parameterName));

				throw new ArgumentNullException(parameterName, message);
			}
		}

		/// <summary>
		/// Will verify that the String is null or empty and throw an null argument exception.  The message parameter is optional.
		/// if( collection == null || !collection.Any() ) throw Exception()
		/// </summary>
		/// <param name="collection">The collection to check for data..</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default won't suffice.</param>
		public static void NullOrEmpty<T>(IEnumerable<T> collection, string parameterName, string message = null)
		{
			if (collection == null || !collection.Any())
			{
				if (message == null)
					throw new ArgumentNullException(parameterName, Formatter.NullOrEmpty(parameterName));

				throw new ArgumentNullException(parameterName, message);
			}
		}

		/// <summary>
		/// Will check the argument if the argument is not a valid id and throw an exception.  Only positive integers are allowed.  The message parameter is optional.
		/// if( value &lt; 1 ) throw Exception()
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentOutOfRangeException won't suffice.</param>
		public static void NotId(Int32 value, String parameterName, String message = null)
		{
			if (value < 1)
			{
				if (message == null)
					throw new ArgumentOutOfRangeException(parameterName, Formatter.NotId(value, parameterName));

				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		/// <summary>
		/// Will check the two values if they are equal and throw exception.  The message parameter is optional.
		/// if( value == shouldNotBe ) throw Exception;
		/// if( value.Equals(shouldNotBe) ) throw Exception; //actually what it does.
		/// </summary>
		/// <typeparam name="T">The type to compare.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="shouldNotBe">The value it should not be.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentOutOfRangeException won't suffice.</param>
		public static void Equals<T>(T value, T shouldNotBe, String parameterName, String message = null)
		{
			if (value.Equals(shouldNotBe))
			{
				if (message == null)
					throw new ArgumentOutOfRangeException(parameterName, Formatter.Equals(value, shouldNotBe, parameterName));

				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		/// <summary>
		/// Will check that the two values equal each other, and if they are not equal, will throw an exception.  The message parameter is optional.
		/// if( value != shouldBe ) throw Exception();
		/// if( false == value.Equals(shouldBe) ) throw Exception();
		/// </summary>
		/// <typeparam name="T">The type to compare.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="shouldBe">The value it should be.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentOutOfRangeException won't suffice.</param>
		public static void NotEquals<T>(T value, T shouldBe, String parameterName, String message = null)
		{
			if (false == value.Equals(shouldBe))
			{
				if (message == null)
					throw new ArgumentOutOfRangeException(parameterName, Formatter.NotEquals(value, shouldBe, parameterName));

				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		/// <summary>
		/// Will check that the value is true, if the value is false, it will throw an exception.  The message parameter is optional.
		/// if( false == value ) throw Exception();
		/// </summary>
		/// <typeparam name="T">The type to compare.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentException won't suffice.</param>
		public static void False(Boolean value, String parameterName, String message = null)
		{
			if (false == value)
			{
				if (message == null)
					throw new ArgumentException(Formatter.False(parameterName), parameterName);

				throw new ArgumentException(message, parameterName);
			}
		}

		/// <summary>
		/// Will check that the value is false, if the value is true, it will throw an exception.  The message parameter is optional.
		/// if( value ) throw Exception();
		/// </summary>
		/// <typeparam name="T">The type to compare.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentException won't suffice.</param>
		public static void True(Boolean value, String parameterName, String message = null)
		{
			if (value)
			{
				if (message == null)
					throw new ArgumentException(Formatter.False(parameterName), parameterName);

				throw new ArgumentException(message, parameterName);
			}
		}

		/// <summary>
		/// Will check that the value is null or an empty collection.  If empty or null, will throw an exception.  The message parameter is optional.
		/// if ( value == null || false == value.Any() ) throw Exception();
		/// </summary>
		/// <typeparam name="T">The type to compare.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentNullException won't suffice.</param>
		public static void Empty<T>(IEnumerable<T> value, String parameterName, String message = null)
		{
			if (value == null || false == value.Any())
			{
				if (message == null)
					throw new ArgumentNullException(parameterName, Formatter.Empty(parameterName));

				throw new ArgumentNullException(parameterName, message);
			}
		}

		/// <summary>
		/// Will check the argument if the argument is not a valid key and throw an exception.  The message parameter is optional.
		/// if (value == Guid.Empty) throw Exception()
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentOutOfRangeException won't suffice.</param>
		public static void NotKey(Guid value, String parameterName, String message = null)
		{
			if (value == Guid.Empty)
			{
				if (message == null)
					throw new ArgumentOutOfRangeException(parameterName, Formatter.NotKey(value, parameterName));

				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}


		/// <summary>
		/// Will check if the attribute is present from a given class instance.
		/// </summary>
		/// <typeparam name="TInstance">The class instance</typeparam>
		/// <param name="requiredAttributeType">the attribute to be checked.</param>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="message">The optional error message if the default ArgumentException won't suffice.</param>
		public static void Attribute<TInstance>(Type requiredAttributeType, String parameterName, String message = null)
			where TInstance : class
		{
			var attribute = typeof(TInstance).GetCustomAttributesByType(requiredAttributeType, false).SingleOrDefault();
			Check.Null(attribute, parameterName, message);
		}

		internal static class Formatter
		{
			/// <summary>
			/// Gets the default error message for the Check.Equals method.
			/// </summary>
			internal static String Equals<T>(T value, T shouldNotBe, String parameterName)
			{
				return "The parameter:" + parameterName + " is not allowed to have value: " + value + ".";
			}

			/// <summary>
			/// Gets the default error message for the Check.NotEquals method.
			/// </summary>
			internal static String NotEquals<T>(T value, T shouldBe, String parameterName)
			{
				return "The parameter: " + parameterName + " had incorrect value.  The value was: " + value + ", but should have been: " + shouldBe + ".";
			}

			/// <summary>
			/// Gets the default error message for the Check.NullOrEmpty method.
			/// </summary>
			internal static String NullOrEmpty(String parameterName)
			{
				return "The parameter: " + parameterName + " was null or empty and needs to have a value or at least not empty.";
			}

			/// <summary>
			/// Gets the default error message for the Check.NotId method.
			/// </summary>
			internal static String NotId(Int32 value, String parameterName)
			{
				return "The parameter: " + parameterName + " with value: " + value + " is not a valid id.  It should be a positive integer.";
			}

			/// <summary>
			/// Gets the default error message for the Check.Null method.
			/// </summary>
			internal static String Null(String parameterName)
			{
				return "The parameter: " + parameterName + " is not allowed to be null.";
			}

			/// <summary>
			/// Gets the default error message for the Check.NotNull method.
			/// </summary>
			internal static String NotNull(String parameterName)
			{
				return "The parameter: " + parameterName + " must be null.";
			}

			/// <summary>
			/// Gets the default error message for the Check.False method.
			/// </summary>
			internal static String False(String parameterName)
			{
				return "The parameter: " + parameterName + " should have been false.";
			}

			/// <summary>
			/// Gets the default error message for the Check.Empty method.
			/// </summary>
			internal static String Empty(String parameterName)
			{
				return "The parameter: " + parameterName + " had a null or empty collection.";
			}

			/// <summary>
			/// Gets the default error message for the Check.NotKey method.
			/// </summary>
			internal static String NotKey(Guid value, String parameterName)
			{
				return "The parameter: " + parameterName + " with value: " + value + " is not a valid key.";
			}
		}
	}
}
