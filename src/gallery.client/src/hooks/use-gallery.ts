import { useEffect, useState } from "react";
import { GalleryDetail } from "../model/api/GalleryDetail";
import axios from "axios";

/**
 * Hook that fetches a gallery, preloads images and returns the loading state.
 */
export const useGallery = (galleryId: string) => {
  const [loadState, setLoadState] = useState({ isLoading: true, loaded: 0, total: 0, error: null as Error | null, gallery: null as GalleryDetail | null });

  useEffect(() => {
    const preloadAbortController = new AbortController();

    const loadImages = async () => {
      try {
        setLoadState({ loaded: 0, total: 0, isLoading: true, error: null, gallery: null });

        const response = await axios.get<GalleryDetail>('/api/galleries/' + galleryId);
        const gallery = response.data;

        setLoadState({ loaded: 0, total: gallery.imageUrls.length, isLoading: true, error: null, gallery });

        for (const url of gallery.imageUrls) {
          if (preloadAbortController.signal.aborted) return;
          const img = new Image();
          img.src = url;

          await img.decode();

          setLoadState(x => ({ ...x, loaded: x.loaded + 1 }))
        }

        setLoadState({ loaded: 0, total: 0, isLoading: false, error: null, gallery });
      }
      catch (err) {
        setLoadState({ loaded: 0, total: 0, isLoading: false, error: err as Error, gallery: null });
      }
    };

    loadImages();

    return () => preloadAbortController.abort();
  }, [galleryId]);

  return loadState
}