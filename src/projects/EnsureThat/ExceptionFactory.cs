﻿using System;
using JetBrains.Annotations;

namespace EnsureThat
{
    public static class ExceptionFactory
    {
        public static ArgumentException CreateForParamValidation(Param param, string message)
            => new ArgumentException(message, param.Name);

        public static ArgumentNullException CreateForParamNullValidation(Param param, string message)
            => new ArgumentNullException(param.Name, message);

        public static Exception CreateForComparableParamValidation<T>(Param<T> param, string message)
        {
            if (param.ExceptionFn != null)
                throw param.ExceptionFn(param);

            return new ArgumentOutOfRangeException(
                param.Name,
                param.Value,
                param.ExtraMessageFn == null
                    ? message
                    : string.Concat(message, Environment.NewLine, param.ExtraMessageFn(param)));
        }

        public static Exception CreateForParamValidation<T>(Param<T> param, string message)
        {
            if (param.ExceptionFn != null)
                throw param.ExceptionFn(param);

            return new ArgumentException(
                param.ExtraMessageFn == null
                    ? message
                    : string.Concat(message, Environment.NewLine, param.ExtraMessageFn(param)),
                param.Name);
        }

        public static Exception CreateForParamNullValidation<T>(Param<T> param, string message)
        {
            if (param.ExceptionFn != null)
                return param.ExceptionFn(param);

            return new ArgumentNullException(
                param.Name,
                param.ExtraMessageFn == null
                    ? message
                    : string.Concat(message, Environment.NewLine, param.ExtraMessageFn(param)));
        }
    }
}