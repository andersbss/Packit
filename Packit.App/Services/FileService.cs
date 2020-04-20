﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Packit.App.Services
{
    public static class FileService
    {
        public static async Task<StorageFile> GetImageFromDevice()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file is null)
                return null;

            return file;
        }

        public static async Task<BitmapImage> StorageFileToBitmapImageAsync(StorageFile storageFile)
        {
            using (IRandomAccessStream fileStream = await storageFile?.OpenAsync(FileAccessMode.Read))
            {
                BitmapImage bitmapImage = new BitmapImage();

                await bitmapImage.SetSourceAsync(fileStream);
                return bitmapImage;
            }
        }
    }
}
