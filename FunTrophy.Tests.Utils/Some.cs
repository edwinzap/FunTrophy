using AutoBogus;
using Bogus;
using System.Net.Mail;
using System.Reflection;

namespace FunTrophy.Tests.Utils
{
    public class Some
    {
        public class StringOverride : AutoGeneratorOverride
        {
            public override bool CanOverride(AutoGenerateContext context)
            {
                return context.GenerateType == typeof(string);
            }

            public override void Generate(AutoGenerateOverrideContext context)
            {
                context.Instance = Some.String();
            }
        }

        public class DateTimeOverride : AutoGeneratorOverride
        {
            public override bool CanOverride(AutoGenerateContext context)
            {
                return context.GenerateType == typeof(DateTime);
            }

            public override void Generate(AutoGenerateOverrideContext context)
            {
                context.Instance = Some.DateTime();
            }
        }

        public class DefaultBinder : AutoBinder
        {
            protected internal readonly Dictionary<Type, MulticastDelegate> TypeRules = new Dictionary<Type, MulticastDelegate>();

            public override TType CreateInstance<TType>(AutoGenerateContext context)
            {
                if (typeof(TType) == typeof(MailAddress))
                {
                    return (TType)(object)MailAddress();
                }

                if (typeof(TType) == typeof(TimeZoneInfo))
                {
                    return (TType)(object)TimeZoneInfo();
                }

                if (TypeRules.TryGetValue(typeof(TType), out MulticastDelegate value))
                {
                    return GeneratedReferenceTypeWithRules<TType>(value);
                }

                return base.CreateInstance<TType>(context);
            }

            public override void PopulateInstance<TType>(object instance, AutoGenerateContext context, IEnumerable<MemberInfo> members = null)
            {
                if (!(typeof(TType) == typeof(MailAddress)) && !(typeof(TType) == typeof(TimeZoneInfo)) && !TypeRules.ContainsKey(typeof(TType)))
                {
                    base.PopulateInstance<TType>(instance, context, members);
                }
            }
        }

        private static DefaultBinder _defaultBinder;

        static Some()
        {
            _defaultBinder = new DefaultBinder();
            AutoFaker.Configure(delegate (IAutoFakerDefaultConfigBuilder c)
            {
                c.WithOverride(new DateTimeOverride()).WithOverride(new StringOverride()).WithTreeDepth(2);
            });
        }

        public static void CustomConfigApplied(DefaultBinder defaultBinder = null)
        {
            if (defaultBinder != null)
            {
                _defaultBinder = defaultBinder;
            }
        }

        public static AutoFaker<TType> InstanceOf<TType>() where TType : class
        {
            if (!_defaultBinder.TypeRules.TryGetValue(typeof(TType), out MulticastDelegate value))
            {
                return new AutoFaker<TType>(_defaultBinder);
            }

            return AutoFakerWithRules<TType>(value, _defaultBinder);
        }

        public static object Generated(Type type)
        {
            try
            {
                return typeof(Some).GetMethods().Single((MethodInfo x) => x.Name == "Generated" && x.GetGenericArguments().Any() && !x.GetParameters().Any()).MakeGenericMethod(type)
                    .Invoke(null, null);
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                throw;
            }
        }

        public static TType Generated<TType>()
        {
            if (typeof(TType).IsValueType)
            {
                return (TType)GeneratedValueType<TType>();
            }

            return GeneratedReferenceType<TType>();
        }

        public static List<TType> Generated<TType>(int count)
        {
            return (from x in Enumerable.Range(1, count)
                    select Generated<TType>()).ToList();
        }

        public static List<T> Generated<T>(int min, int max)
        {
            return Generated<T>(Int(min, max));
        }

        public static TType From<TType>(params TType[] possibleValues)
        {
            if (!possibleValues.Any())
            {
                throw new ArgumentException("Possible values must be provided.");
            }

            return possibleValues[Int(0, possibleValues.Length - 1)];
        }

        public static TType Except<TType>(params TType[] excludedValues)
        {
            if (!excludedValues.Any())
            {
                throw new ArgumentException("Excluded values must be provided.");
            }

            TType val = Generated<TType>();
            int num = 1;
            while (excludedValues.Contains(val))
            {
                if (num > 9)
                {
                    throw new ArgumentException("Impossible to generate a value, without using the excluded values, within a reasonable amount of tries.");
                }

                val = Generated<TType>();
                num++;
            }

            return val;
        }

        public static List<TEnum> UniqueValues<TEnum>(int length) where TEnum : Enum
        {
            if (Enum.GetValues(typeof(TEnum)).Length < length)
            {
                throw new ArgumentException("Not enough possible values.");
            }

            List<TEnum> list = new List<TEnum>();
            for (int i = 0; i < length; i++)
            {
                TEnum item = Generated<TEnum>();
                while (list.Contains(item))
                {
                    item = Except(list.ToArray());
                }

                list.Add(item);
            }

            return list;
        }

        public static List<TEnum> UniqueValues<TEnum>(int min, int max) where TEnum : Enum
        {
            return UniqueValues<TEnum>(Int(min, max));
        }

        private static TEnum GeneratedEnum<TEnum>(Faker faker)
        {
            return (TEnum)(from x in typeof(Faker).GetMethods()
                           where x.Name == "PickRandom"
                           select x).Single((MethodInfo x) => !x.GetParameters().Any()).MakeGenericMethod(typeof(TEnum)).Invoke(faker, null);
        }

        private static object GeneratedValueType<TValueType>()
        {
            Faker faker = new Faker();
            Type typeFromHandle = typeof(TValueType);
            Type underlyingType = Nullable.GetUnderlyingType(typeFromHandle);
            if (underlyingType != null)
            {
                return typeof(Some).GetMethod("GeneratedValueType", BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(underlyingType).Invoke(null, null);
            }

            if (typeFromHandle.IsEnum)
            {
                return GeneratedEnum<TValueType>(faker);
            }

            return AutoFaker.Generate<TValueType>();
        }

        private static TReferenceType GeneratedReferenceType<TReferenceType>()
        {
            try
            {
                return (TReferenceType)typeof(Some).GetMethod("ConstrainedGeneratedReferenceType", BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(TReferenceType)).Invoke(null, null);
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                throw;
            }
        }

        private static T ConstrainedGeneratedReferenceType<T>() where T : class
        {
            try
            {
                AutoFaker<T> autoFaker = InstanceOf<T>();
                return (T)(from y in autoFaker.GetType().GetMethods()
                           where y.Name == "Generate"
                           select y).Single((MethodInfo y) => y.GetParameters().Length == 1).Invoke(autoFaker, new object[1]);
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                throw;
            }
        }

        private static TReferenceType GeneratedReferenceTypeWithRules<TReferenceType>(MulticastDelegate typeRules)
        {
            try
            {
                return (TReferenceType)typeof(Some).GetMethod("ConstrainedGeneratedReferenceTypeWithRules", BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(TReferenceType)).Invoke(null, new object[1]
                {
                    typeRules
                });
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
                throw;
            }
        }

        private static T ConstrainedGeneratedReferenceTypeWithRules<T>(MulticastDelegate typeRules) where T : class
        {
            return AutoFakerWithRules<T>(typeRules).Generate();
        }

        private static AutoFaker<TType> AutoFakerWithRules<TType>(MulticastDelegate typeRules, IAutoBinder binder = null) where TType : class
        {
            AutoFaker<TType> arg = (binder != null) ? new AutoFaker<TType>(binder) : new AutoFaker<TType>();
            return (AutoFaker<TType>)((Func<Faker<TType>, Faker<TType>>)typeRules)(arg);
        }

        public static string String(int length = 0)
        {
            if (length > 0)
            {
                return new Faker().Random.AlphaNumeric(length);
            }

            return String(40, 80);
        }

        public static string String(int minLength, int maxLength)
        {
            return String(Int(minLength, maxLength));
        }

        public static string CompanyName()
        {
            return new Faker().Company.CompanyName();
        }

        public static MailAddress MailAddress()
        {
            return new Faker<MailAddress>().CustomInstantiator((Faker f) => new MailAddress(f.Internet.Email() ?? "")).Generate();
        }

        public static TimeZoneInfo TimeZoneInfo()
        {
            return new Faker().PickRandomParam(System.TimeZoneInfo.GetSystemTimeZones().ToArray());
        }

        public static int Int(int min = int.MinValue, int max = int.MaxValue)
        {
            return new Faker().Random.Int(min, max);
        }

        public static bool Bool()
        {
            return new Faker().Random.Bool();
        }

        public static DateTime DateTime()
        {
            Faker faker = new Faker();
            if (!faker.Random.Bool())
            {
                return faker.Date.Future();
            }

            return faker.Date.Past();
        }

        public static DateTime DateTimeAfter(DateTime refDate)
        {
            return new Faker().Date.Future(1, refDate);
        }

        public static DateTime DateTimeBefore(DateTime refDate)
        {
            return new Faker().Date.Past(1, refDate);
        }

        public static DateTime DateTimeBetween(DateTime start, DateTime end)
        {
            return new Faker().Date.Between(start, end);
        }
    }
}