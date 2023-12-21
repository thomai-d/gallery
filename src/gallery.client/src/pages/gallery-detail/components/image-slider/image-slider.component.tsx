import React, { useState } from 'react';
import classes from './image-slider.component.module.scss'

interface ImageSliderProps {
  imageUrls: string[];
}

export const ImageSlider: React.FC<ImageSliderProps> = ({ imageUrls }) => {

  const [selectedImage, setSelectedImage] = useState<string | null>(null);

  const handleSliderChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const sliderValue = Number(event.target.value);
    setSelectedImage(imageUrls[sliderValue]);
  };

  const imageUrl = selectedImage || imageUrls[0]
  const imageName = imageUrl.split('/').slice(-1)

  return (
    <div className={classes.container}>
      <img className={classes.image} src={imageUrl} alt="slider" />

      <div className={classes.legend}>
        <span>{imageName}</span>
      </div>

      <input className={classes.slider}
        type="range"
        min="0"
        max={imageUrls.length - 1}
        defaultValue="0"
        onChange={handleSliderChange}
      />
    </div >
  );
};