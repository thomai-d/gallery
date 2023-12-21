import axios from 'axios'
import { useQuery } from 'react-query';
import { GallerySummary } from '../model/api/GallerySummary';

/**
 * Hook to fetch all galleries.
 */
export const useGalleries = () => {
  return useQuery("galleries", async () => {
    const { data } = await axios.get<GallerySummary[]>('/api/galleries')
    return data;
  })
}