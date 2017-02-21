struct ArrayListNewCudaPixelData
{
	__device__  ArrayListNewCudaPixelData()
	{
	}
	unsigned char blue;
	unsigned char green;
	unsigned char red;
	unsigned char alpha;
};


// H264Images.ArrayListNewCuda
extern "C" __global__  void calGPU( unsigned char* dev_bitmap1, int dev_bitmap1Len0,  unsigned char* dev_bitmap2, int dev_bitmap2Len0,  unsigned char* dev_result, int dev_resultLen0,  int* imageWidth, int imageWidthLen0,  int* count, int countLen0,  int* possition, int possitionLen0);

// H264Images.ArrayListNewCuda
extern "C" __global__  void calGPU( unsigned char* dev_bitmap1, int dev_bitmap1Len0,  unsigned char* dev_bitmap2, int dev_bitmap2Len0,  unsigned char* dev_result, int dev_resultLen0,  int* imageWidth, int imageWidthLen0,  int* count, int countLen0,  int* possition, int possitionLen0)
{
	int i = blockIdx.x * blockDim.x + threadIdx.x;
	int j = blockIdx.y * blockDim.y + threadIdx.y;
	__shared__ int array[2];

	int arrayLen0 = 2;
	array[(0)] = 0;
	array[(1)] = 0;
	for (i = 0; i < imageWidth[(1)]; i++)
	{
		for (j = 0; j < imageWidth[(0)]; j++)
		{
			int num = (i * imageWidth[(0)] + j) * 4;
			ArrayListNewCudaPixelData pixelData = ArrayListNewCudaPixelData();
			ArrayListNewCudaPixelData pixelData2 = ArrayListNewCudaPixelData();
			pixelData.red = dev_bitmap1[(num + 2)];
			pixelData.green = dev_bitmap1[(num + 1)];
			pixelData.blue = dev_bitmap1[(num)];
			pixelData.alpha = dev_bitmap1[(num + 3)];
			pixelData2.green = dev_bitmap2[(num + 1)];
			pixelData2.red = dev_bitmap2[(num + 2)];
			pixelData2.blue = dev_bitmap2[(num)];
			pixelData2.alpha = dev_bitmap2[(num + 3)];
			bool flag = pixelData.red > pixelData2.red;
			int num2;
			if (flag)
			{
				num2 = (int)(pixelData.red - pixelData2.red);
			}
			else
			{
				num2 = (int)(pixelData2.red - pixelData.red);
			}
			bool flag2 = pixelData.alpha > pixelData2.alpha;
			int num3;
			if (flag2)
			{
				num3 = (int)(pixelData.alpha - pixelData2.alpha);
			}
			else
			{
				num3 = (int)(pixelData2.alpha - pixelData.alpha);
			}
			bool flag3 = pixelData.green > pixelData2.green;
			int num4;
			if (flag3)
			{
				num4 = (int)(pixelData.green - pixelData2.green);
			}
			else
			{
				num4 = (int)(pixelData2.green - pixelData.green);
			}
			bool flag4 = pixelData.blue > pixelData2.blue;
			int num5;
			if (flag4)
			{
				num5 = (int)(pixelData.blue - pixelData2.blue);
			}
			else
			{
				num5 = (int)(pixelData2.blue - pixelData.blue);
			}
			bool flag5 = num2 > 8 || num3 > 8 || num4 > 8 || num5 > 8;
			if (flag5)
			{
				 int* expr_1F9_cp_0 = array;
				int expr_1F9_cp_1 = 1;
				int num6 = expr_1F9_cp_0[(expr_1F9_cp_1)];
				expr_1F9_cp_0[(expr_1F9_cp_1)] = num6 + 1;
				possition[(num6)] = i;
				 int* expr_20F_cp_0 = array;
				int expr_20F_cp_1 = 1;
				num6 = expr_20F_cp_0[(expr_20F_cp_1)];
				expr_20F_cp_0[(expr_20F_cp_1)] = num6 + 1;
				possition[(num6)] = j;
				 int* expr_224_cp_0 = array;
				int expr_224_cp_1 = 0;
				num6 = expr_224_cp_0[(expr_224_cp_1)];
				expr_224_cp_0[(expr_224_cp_1)] = num6 + 1;
				dev_result[(num6)] = pixelData2.blue;
				 int* expr_23F_cp_0 = array;
				int expr_23F_cp_1 = 0;
				num6 = expr_23F_cp_0[(expr_23F_cp_1)];
				expr_23F_cp_0[(expr_23F_cp_1)] = num6 + 1;
				dev_result[(num6)] = pixelData2.green;
				 int* expr_25A_cp_0 = array;
				int expr_25A_cp_1 = 0;
				num6 = expr_25A_cp_0[(expr_25A_cp_1)];
				expr_25A_cp_0[(expr_25A_cp_1)] = num6 + 1;
				dev_result[(num6)] = pixelData2.red;
				 int* expr_275_cp_0 = array;
				int expr_275_cp_1 = 0;
				num6 = expr_275_cp_0[(expr_275_cp_1)];
				expr_275_cp_0[(expr_275_cp_1)] = num6 + 1;
				dev_result[(num6)] = pixelData2.alpha;
				count[(1)] = array[(1)];
				count[(0)] = array[(0)];
			}
		}
	}
}
