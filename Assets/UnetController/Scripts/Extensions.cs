﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace GreenByteSoftware.UNetController {
	public static class Extensions {

		static long guidCount;

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite1<T> {
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public byte v1;
		}

		public static void Write1<T>(NetworkWriter writer, T value) {
			BWrite1<T> proxy = default(BWrite1<T>);
			proxy.val = value;
			writer.Write(proxy.v1);
		}

		public static T Read1<T>(NetworkReader reader) {
			BWrite1<T> proxy = default(BWrite1<T>);
			proxy.v1 = reader.ReadByte();
			return proxy.val;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite2<T>
		{
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public short v1;
		}

		public static void Write2<T>(NetworkWriter writer, T value) {
			BWrite2<T> proxy = default(BWrite2<T>);
			proxy.val = value;
			writer.Write(proxy.v1);
		}

		public static T Read2<T>(NetworkReader reader) {
			BWrite2<T> proxy = default(BWrite2<T>);
			proxy.v1 = reader.ReadInt16();
			return proxy.val;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite4<T>
		{
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public uint v1;
		}

		public static void Write4<T>(NetworkWriter writer, T value) {
			BWrite4<T> proxy = default(BWrite4<T>);
			proxy.val = value;
			writer.WritePackedUInt32(proxy.v1);
		}

		public static T Read4<T>(NetworkReader reader) {
			BWrite4<T> proxy = default(BWrite4<T>);
			proxy.v1 = reader.ReadPackedUInt32();
			return proxy.val;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite8<T>
		{
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public ulong v1;
		}

		public static void Write8<T>(NetworkWriter writer, T value) {
			BWrite8<T> proxy = default(BWrite8<T>);
			proxy.val = value;
			writer.WritePackedUInt64(proxy.v1);
		}

		public static T Read8<T>(NetworkReader reader) {
			BWrite8<T> proxy = default(BWrite8<T>);
			proxy.v1 = reader.ReadPackedUInt64();
			return proxy.val;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite12<T>
		{
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public ulong v1;
			[FieldOffset(8)]
			public uint v2;
		}

		public static void Write12<T>(NetworkWriter writer, T value) {
			BWrite12<T> proxy = default(BWrite12<T>);
			proxy.val = value;
			writer.WritePackedUInt64(proxy.v1);
			writer.WritePackedUInt64(proxy.v2);
		}

		public static T Read12<T>(NetworkReader reader) {
			BWrite12<T> proxy = default(BWrite12<T>);
			proxy.v1 = reader.ReadPackedUInt64();
			proxy.v2 = reader.ReadPackedUInt32();
			return proxy.val;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct BWrite16<T>
		{
			[FieldOffset(0)]
			public T val;

			[FieldOffset(0)]
			public ulong v1;
			[FieldOffset(8)]
			public ulong v2;
		}

		public static void Write16<T>(NetworkWriter writer, T value) {
			BWrite16<T> proxy = default(BWrite16<T>);
			proxy.val = value;
			writer.WritePackedUInt64(proxy.v1);
			writer.WritePackedUInt64(proxy.v2);
		}

		public static T Read16<T>(NetworkReader reader) {
			BWrite16<T> proxy = default(BWrite16<T>);
			proxy.v1 = reader.ReadPackedUInt64();
			proxy.v2 = reader.ReadPackedUInt64();
			return proxy.val;
		}

		public static string ResultsCompared (ref Results r1, ref Results r2) {
			return "" + r1.position + "\t|\t" + r2.position + "\t|\t" + (r1.position - r2.position) + "\n"
				+ r1.rotation.eulerAngles + "\t|\t" + r2.rotation.eulerAngles + "\t|\t" + (r1.rotation.eulerAngles - r2.rotation.eulerAngles) + "\n"
				+ r1.camX + "\t|\t" + r2.camX + "\t|\t" + (r1.camX - r2.camX) + "\n"
				+ r1.speed + "\t|\t" + r2.speed + "\t|\t" + (r1.speed - r2.speed) + "\n"
				+ r1.flags + "\t\t|\t" + r2.flags + "\t|\t" + (r1.flags == r2.flags) + "\n"
				+ r1.groundPoint + "\t|\t" + r2.groundPoint + "\t|\t" + (r1.groundPoint- r2.groundPoint) + "\n"
				+ r1.groundPointTime + "\t|\t" + r2.groundPointTime + "\t|\t" + (r1.groundPointTime - r2.groundPointTime) + "\n"
				+ r1.timestamp + "\t\t|\t" + r2.timestamp + "\n";
		}

		public static string GenerateGUID(){

			DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
			double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
			guidCount++;
			string uniqueID = Application.systemLanguage +"-"+Application.platform.ToString() 
				+"-"+String.Format("{0:X}", (int) timestamp)
				+"-"+String.Format("{0:X}", (int) (Time.time*1000000))
				+"-"+String.Format("{0:X}", UnityEngine.Random.Range(1000000,9999999))
				+"-"+String.Format("{0:X}", guidCount);

			return uniqueID;
		}

		public static bool AlmostEquals(this float float1, float float2, float precision)
		{
			return (Mathf.Abs(float1 - float2) <= precision);
		}

		[System.Serializable]
		public struct SVector2 {
			public float x;
			public float y;

			public SVector2 (Vector2 value) {
				x = value.x;
				y = value.y;
			}

			public SVector2 (float newX, float newY) {
				x = newX;
				y = newY;
			}

			public SVector2 (int newX, int newY) {
				x = (float)newX;
				y = (float)newY;
			}

			static public implicit operator Vector2(SVector2 value) {
				return new Vector2 (value.x, value.y);
			}

			static public implicit operator SVector2(Vector2 value) {
				return new SVector2 (value);
			}

			static public implicit operator string(SVector2 value) {
				return "( " + value.x + ", " + value.y + " )";
			}
		}

		[System.Serializable]
		public struct SVector3 {
			public float x;
			public float y;
			public float z;

			public SVector3 (Vector3 value) {
				x = value.x;
				y = value.y;
				z = value.z;
			}

			public SVector3 (float newX, float newY, float newZ) {
				x = newX;
				y = newY;
				z = newZ;
			}

			static public implicit operator Vector3(SVector3 value) {
				return new Vector3 (value.x, value.y, value.z);
			}

			static public implicit operator SVector3(Vector3 value) {
				return new SVector3 (value);
			}

			static public implicit operator string(SVector3 value) {
				return "( " + value.x + ", " + value.y + ", " + value.z + " )";
			}
		}

		[System.Serializable]
		public struct SVector4 {
			public float x;
			public float y;
			public float z;
			public float w;

			public SVector4 (Vector4 value) {
				x = value.x;
				y = value.y;
				z = value.z;
				w = value.w;
			}

			public SVector4 (float newX, float newY, float newZ, float newW) {
				x = newX;
				y = newY;
				z = newZ;
				w = newW;
			}

			public SVector4 (int newX, int newY, int newZ, int newW) {
				x = (float)newX;
				y = (float)newY;
				z = (float)newZ;
				w = (float)newW;
			}

			static public implicit operator Vector4(SVector4 value) {
				return new Vector4 (value.x, value.y, value.z, value.w);
			}

			static public implicit operator SVector4(Vector4 value) {
				return new SVector4 (value);
			}

			static public implicit operator string(SVector4 value) {
				return "( " + value.x + ", " + value.y + ", " + value.z + ", " + value.w + " )";
			}
		}

		[System.Serializable]
		public struct SQuaternion {
			public float x;
			public float y;
			public float z;
			public float w;

			public SQuaternion (Quaternion value) {
				x = value.x;
				y = value.y;
				z = value.z;
				w = value.w;
			}

			public SQuaternion (float newX, float newY, float newZ, float newW) {
				x = newX;
				y = newY;
				z = newZ;
				w = newW;
			}

			public SQuaternion (int newX, int newY, int newZ, int newW) {
				x = (float)newX;
				y = (float)newY;
				z = (float)newZ;
				w = (float)newW;
			}

			static public implicit operator Quaternion(SQuaternion value) {
				return new Quaternion (value.x, value.y, value.z, value.w);
			}

			static public implicit operator SQuaternion(Quaternion value) {
				return new SQuaternion (value);
			}

			static public implicit operator string(SQuaternion value) {
				return "( " + value.x + ", " + value.y + ", " + value.z + ", " + value.w + " )";
			}
		}

		public static void Set(ref byte byteVal, int pos, bool value) {
			if (value)
				byteVal = (byte)(byteVal | (1 << pos));
			else
				byteVal = (byte)(byteVal & ~(1 << pos));
		}

		public static bool Get(int val, int pos) {
			return ((val & (1 << pos)) != 0);
		}

		//A hack for converting between ints and float without changing bytes.
		[StructLayout(LayoutKind.Explicit)]
		public struct FloatAndUIntUnion
		{
			[FieldOffset(0)]
			public uint  UInt32Bits;
			[FieldOffset(0)]
			public int  Int32Bits;
			[FieldOffset(0)]
			public float FloatValue;

			public FloatAndUIntUnion(float val) {
				UInt32Bits = 0;
				Int32Bits = 0;
				FloatValue = val;
			}

			public FloatAndUIntUnion(int val) {
				UInt32Bits = 0;
				FloatValue = 0;
				Int32Bits = val;
			}

			public FloatAndUIntUnion(uint val) {
				Int32Bits = 0;
				FloatValue = 0;
				UInt32Bits = val;
			}
		}

		//Non-alloc implementations of GetBytes
		public static void GetBytes (float value, byte[] bytes) {
			GetBytes((new FloatAndUIntUnion (value)).Int32Bits, bytes);
		}

		public static void GetBytes (int value, byte[] bytes) {

			if ((value & (1 << 0)) != 0)
				bytes [0] |= (1 << 0);
			else
				bytes [0] &= byte.MaxValue ^ (1 << 0);

			if ((value & (1 << 1)) != 0)
				bytes [0] |= (1 << 1);
			else
				bytes [0] &= byte.MaxValue ^ (1 << 1);

			if ((value & (1 << 2)) != 0)
				bytes [0] |= (1 << 2);
			else
				bytes [0] &= byte.MaxValue ^ (1 << 2);

			if ((value & (1 << 3)) != 0)
				bytes [0] |= (1 << 3);
			else
				bytes [0] &= byte.MaxValue ^ (1 << 3);

			if ((value & (1 << 4)) != 0)
				bytes [0] |= 4;
			else
				bytes [0] &= byte.MaxValue ^ (1 << 4);

			if ((value & (1 << 5)) != 0)
				bytes [0] |= 5;
			else
				bytes [0] &= byte.MaxValue ^ (1 << 5);

			if ((value & (1 << 6)) != 0)
				bytes [0] |= 6;
			else
				bytes [0] &= byte.MaxValue ^ (1 << 6);

			if ((value & (1 << 7)) != 0)
				bytes [0] |= 7;
			else
				bytes [0] &= byte.MaxValue ^ (1 << 7);


			if ((value & (1 << 8)) != 0)
				bytes [1] |= (1 << 0);
			else
				bytes [1] &= byte.MaxValue ^ (1 << 0);

			if ((value & (1 << 9)) != 0)
				bytes [1] |= (1 << 1);
			else
				bytes [1] &= byte.MaxValue ^ (1 << 1);

			if ((value & (1 << 10)) != 0)
				bytes [1] |= (1 << 2);
			else
				bytes [1] &= byte.MaxValue ^ (1 << 2);

			if ((value & (1 << 11)) != 0)
				bytes [1] |= (1 << 3);
			else
				bytes [1] &= byte.MaxValue ^ (1 << 3);

			if ((value & (1 << 12)) != 0)
				bytes [1] |= 4;
			else
				bytes [1] &= byte.MaxValue ^ (1 << 4);

			if ((value & (1 << 13)) != 0)
				bytes [1] |= 5;
			else
				bytes [1] &= byte.MaxValue ^ (1 << 5);

			if ((value & (1 << 14)) != 0)
				bytes [1] |= 6;
			else
				bytes [1] &= byte.MaxValue ^ (1 << 6);

			if ((value & (1 << 15)) != 0)
				bytes [1] |= 7;
			else
				bytes [1] &= byte.MaxValue ^ (1 << 7);


			if ((value & (1 << 16)) != 0)
				bytes [2] |= (1 << 0);
			else
				bytes [2] &= byte.MaxValue ^ (1 << 0);

			if ((value & (1 << 17)) != 0)
				bytes [2] |= (1 << 1);
			else
				bytes [2] &= byte.MaxValue ^ (1 << 1);

			if ((value & (1 << 18)) != 0)
				bytes [2] |= (1 << 2);
			else
				bytes [2] &= byte.MaxValue ^ (1 << 2);

			if ((value & (1 << 19)) != 0)
				bytes [2] |= (1 << 3);
			else
				bytes [2] &= byte.MaxValue ^ (1 << 3);

			if ((value & (1 << 20)) != 0)
				bytes [2] |= 4;
			else
				bytes [2] &= byte.MaxValue ^ (1 << 4);

			if ((value & (1 << 21)) != 0)
				bytes [2] |= 5;
			else
				bytes [2] &= byte.MaxValue ^ (1 << 5);

			if ((value & (1 << 22)) != 0)
				bytes [2] |= 6;
			else
				bytes [2] &= byte.MaxValue ^ (1 << 6);

			if ((value & (1 << 23)) != 0)
				bytes [2] |= 7;
			else
				bytes [2] &= byte.MaxValue ^ (1 << 7);


			if ((value & (1 << 24)) != 0)
				bytes [3] |= (1 << 0);
			else
				bytes [3] &= byte.MaxValue ^ (1 << 0);

			if ((value & (1 << 25)) != 0)
				bytes [3] |= (1 << 1);
			else
				bytes [3] &= byte.MaxValue ^ (1 << 1);

			if ((value & (1 << 26)) != 0)
				bytes [3] |= (1 << 2);
			else
				bytes [3] &= byte.MaxValue ^ (1 << 2);

			if ((value & (1 << 27)) != 0)
				bytes [3] |= (1 << 3);
			else
				bytes [3] &= byte.MaxValue ^ (1 << 3);

			if ((value & (1 << 28)) != 0)
				bytes [3] |= 4;
			else
				bytes [3] &= byte.MaxValue ^ (1 << 4);

			if ((value & (1 << 29)) != 0)
				bytes [3] |= 5;
			else
				bytes [3] &= byte.MaxValue ^ (1 << 5);

			if ((value & (1 << 30)) != 0)
				bytes [3] |= 6;
			else
				bytes [3] &= byte.MaxValue ^ (1 << 6);

			if ((value & (1 << 31)) != 0)
				bytes [3] |= 7;
			else
				bytes [3] &= byte.MaxValue ^ (1 << 7);
		}

		public static float ToSingle (byte[] bytes, int index) {
			if (System.BitConverter.IsLittleEndian)
				return System.BitConverter.ToSingle(bytes, index);
			else
				return System.BitConverter.ToSingle(Reverse(bytes), bytes.Length - sizeof(float) - index);
		}

		public static long ToInt64 (byte[] bytes, int index) {
			if (System.BitConverter.IsLittleEndian)
				return System.BitConverter.ToInt64(bytes, index);
			else
				return System.BitConverter.ToInt64(Reverse(bytes), bytes.Length - sizeof(long) - index);
		}

		public static int ToInt32 (byte[] bytes, int index) {
			if (System.BitConverter.IsLittleEndian)
				return System.BitConverter.ToInt32(bytes, index);
			else
				return System.BitConverter.ToInt32(Reverse(bytes), bytes.Length - sizeof(int) - index);
		}

		public static T[] Reverse<T> (T[] array) {
			T[] ret = new T[array.Length];
			for (int i = 0; i < array.Length; i++)
				ret [array.Length - 1 - i] = array [i];
			return ret;
		}

		public static float ClampAngle(this float angle) {
			return Mathf.Repeat(angle, 360f);
		}
	}
}
