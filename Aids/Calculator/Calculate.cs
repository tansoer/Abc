using System;
using System.Collections.Generic;
using Abc.Aids.Methods;
using Abc.Aids.Values;
using System.Linq.Expressions;

namespace Abc.Aids.Calculator {
    public class Calculate {
        private readonly Stack<object> stack;
        public Calculate() => stack = new Stack<object>();
        public object Get() => Safe.Run(() => stack.Pop(), null, true);
        public object Peek() => Safe.Run(() => stack.Peek(), null, true);
        public void Set(object x) => Safe.Run(() => stack.Push(x), true);
        public void Clear() => stack.Clear();
        public void Perform(Operation o) {
            switch (o) {
                case Operation.Add: Add(); break;
                case Operation.Subtract: Subtract(); break;
                case Operation.Multiply: Multiply(); break;
                case Operation.Divide: Divide(); break;
                case Operation.Power: Power(); break;
                case Operation.Inverse: Inverse(); break;
                case Operation.Opposite: Opposite(); break;
                case Operation.Square: Square(); break;
                case Operation.Sqrt: Sqrt(); break;
                case Operation.And: And(); break;
                case Operation.Or: Or(); break;
                case Operation.Xor: Xor(); break;
                case Operation.Not: Not(); break;
                case Operation.IsEqual: IsEqual(); break;
                case Operation.IsGreater: IsGreater(); break;
                case Operation.IsLess: IsLess(); break;
                case Operation.GetYear: GetYear(); break;
                case Operation.GetMonth: GetMonth(); break;
                case Operation.GetDay: GetDay(); break;
                case Operation.GetHour: GetHour(); break;
                case Operation.GetMinute: GetMinute(); break;
                case Operation.GetSecond: GetSecond(); break;
                case Operation.GetAge: GetAge(); break;
                case Operation.GetInterval: GetInterval(); break;
                case Operation.ToYears: ToYears(); break;
                case Operation.ToMonths: ToMonths(); break;
                case Operation.ToDays: ToDays(); break;
                case Operation.ToHours: ToHours(); break;
                case Operation.ToMinutes: ToMinutes(); break;
                case Operation.ToSeconds: ToSeconds(); break;
                case Operation.AddSeconds: AddSeconds(); break;
                case Operation.AddMinutes: AddMinutes(); break;
                case Operation.AddHours: AddHours(); break;
                case Operation.AddDays: AddDays(); break;
                case Operation.AddMonths: AddMonths(); break;
                case Operation.AddYears: AddYears(); break;
                case Operation.GetLength: GetLength(); break;
                case Operation.ToUpper: ToUpper(); break;
                case Operation.ToLower: ToLower(); break;
                case Operation.Trim: Trim(); break;
                case Operation.Substring: Substring(); break;
                case Operation.Contains: Contains(); break;
                case Operation.EndsWith: EndsWith(); break;
                case Operation.StartsWith: StartsWith(); break;
                default: Dummy(); break;
            }
        }
        public void Perform(Operation o, Expression param) {
            switch (o) {
                case Operation.ToContainsExpression: ToExpression(Operation.Contains, param); break;
                case Operation.ToEqualsExpression: ToExpression(Operation.IsEqual, param); break;
                case Operation.Contains: ToExpression(o, param); break;
                case Operation.IsEqual: ToExpression(o, param); break;
                case Operation.EndsWith: ToExpression(o, param); break;
                case Operation.StartsWith: ToExpression(o, param); break;
                default: Dummy(); break;
            }
        }
        public void Dummy() { }
        public virtual void Add() => perform(Compute.Add);
        public virtual void Subtract() => perform(Compute.Subtract);
        public virtual void Multiply() => perform(Compute.Multiply);
        public virtual void Divide() => perform(Compute.Divide);
        public virtual void Power() => perform(Compute.Power);
        public virtual void Opposite() => perform(Compute.Opposite);
        public virtual void Sqrt() => perform(Compute.Sqrt);
        public virtual void Square() => perform(Compute.Square);
        public virtual void Inverse() => perform(Compute.Inverse);
        public virtual void And() => perform(Compute.And);
        public virtual void Or() => perform(Compute.Or);
        public virtual void Xor() => perform(Compute.Xor);
        public virtual void Not() => perform(Compute.Not);
        public virtual void IsEqual() => perform(Compute.IsEqual);
        public virtual void IsGreater() => perform(Compute.IsGreater);
        public virtual void IsLess() => perform(Compute.IsLess);
        public virtual void GetYear() => perform(Compute.GetYear);
        public virtual void GetMonth() => perform(Compute.GetMonth);
        public virtual void GetDay() => perform(Compute.GetDay);
        public virtual void GetHour() => perform(Compute.GetHour);
        public virtual void GetMinute() => perform(Compute.GetMinute);
        public virtual void GetSecond() => perform(Compute.GetSecond);
        public virtual void AddDays() => perform(Compute.AddDays);
        public virtual void AddHours() => perform(Compute.AddHours);
        public virtual void AddMinutes() => perform(Compute.AddMinutes);
        public virtual void AddMonths() => perform(Compute.AddMonths);
        public virtual void AddSeconds() => perform(Compute.AddSeconds);
        public virtual void AddYears() => perform(Compute.AddYears);
        public virtual void GetAge() => perform(Compute.GetAge);
        public virtual void GetInterval() => perform(Compute.GetInterval);
        public virtual void ToYears() => perform(Compute.ToYears);
        public virtual void ToMonths() => perform(Compute.ToMonths);
        public virtual void ToDays() => perform(Compute.ToDays);
        public virtual void ToHours() => perform(Compute.ToHours);
        public virtual void ToMinutes() => perform(Compute.ToMinutes);
        public virtual void ToSeconds() => perform(Compute.ToSeconds);
        public virtual void Contains() => perform(Compute.Contains);
        public virtual void EndsWith() => perform(Compute.EndsWith);
        public virtual void GetLength() => perform(Compute.GetLength);
        public virtual void StartsWith() => perform(Compute.StartsWith);
        public virtual void ToLower() => perform(Compute.ToLower);
        public virtual void ToUpper() => perform(Compute.ToUpper);
        public virtual void Trim() => perform(Compute.Trim);
        public virtual void ToExpression(Operation o, Expression param) => perform(Compute.ToExpression, o.ToString(), param);
        public virtual void Substring() => Safe.Run(() => {
            var y = Get();
            var x = Get();
            Set(TypeIs.String(x) ? Compute.Substring(x, y) : Compute.Substring(Get(), x, y));
        });
        protected virtual void perform(Func<object, object, object> op) => Safe.Run(() => {
            var y = Get();
            Set(op(Get(), y));
        });
        protected virtual void perform(Func<object, object, bool> op) => Safe.Run(() => {
            var y = Get();
            Set(op(Get(), y));
        });
        protected virtual void perform(Func<object, object, string, Expression, object> op, string operation, Expression param)
            => Safe.Run(() => {
                var y = Get();
                Set(op(Get(), y, operation, param));
            });
        protected virtual void perform(Func<object, object> op) => Safe.Run(() => Set(op(Get())));
    }
}
